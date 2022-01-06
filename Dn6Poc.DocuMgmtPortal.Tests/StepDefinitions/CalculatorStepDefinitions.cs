namespace Dn6Poc.DocuMgmtPortal.Tests.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the first number is (.*)")]
        public void GivenTheFirstNumberIs(int number)
        {
            _scenarioContext.Add("firstNumber", number);
        }

        [Given("the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            _scenarioContext.Add("secondNumber", number);
        }

        [When("the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            if (_scenarioContext.TryGetValue<int>("firstNumber", out int firstNumber)
                && _scenarioContext.TryGetValue<int>("secondNumber", out int secondNumber))
                _scenarioContext.Add("result", firstNumber + secondNumber);
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            if (_scenarioContext.TryGetValue<int>("result", out int calculatedResult))
                Assert.AreEqual<int>(result, calculatedResult);
        }
    }
}