using System;
using RestSharpSpecflow.Drivers;
using TechTalk.SpecFlow;
using System;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using RestSharpSpecflow.Drivers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace RestSharpSpecflow.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static ExtentReports _extentReports;
        private ExtentTest _scenario;

        public Hooks(ScenarioContext scenarioContext , FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void InitializeExtentReports()
        {
           // var extentReportPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/ExtentReport.html";
           var extentReportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Report/extent.html");
            _extentReports = new ExtentReports();
            var spark = new ExtentSparkReporter(extentReportPath);
            _extentReports.AttachReporter(spark);
        }


       [BeforeScenario()]
        public void InitializeDriver()
        {
            Driver driver = new Driver(_scenarioContext);
            var feature = _extentReports.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }
        
        
        [AfterStep]
        public void AfterStep()
        {
            if(_scenarioContext.TestError == null)
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        break;
                }
            else
                switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
                {
                    case StepDefinitionType.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail();
                        break;
                    case StepDefinitionType.When:
                        _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail();
                        break;
                    case StepDefinitionType.Then:
                        _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail();
                        break;
                    default:
                        break;
                }
            
                
        }
        
        [AfterTestRun]
        public static void CloseExtentReports()
        {
            _extentReports.Flush();
        }

        
        
    }
}