using DSA.Stack.UseCaseSamples;

namespace Test.Stack
{
    public class BrowserHistoryTests
    {
        [Fact]
        public void Visit_AddsToBackHistory()
        {
            // Arrange
            string initialUrl = "https://www.example.com";
            BrowserHistory browserHistory = new BrowserHistory(initialUrl);

            // Act
            browserHistory.Visit("https://www.google.com");

            // Assert
            Assert.Single(browserHistory.backHistory);
            Assert.Empty(browserHistory.forwardHistory);
        }

        [Fact]
        public void Back_GoesToPreviousPage()
        {
            // Arrange
            string initialUrl = "https://www.example.com";
            BrowserHistory browserHistory = new BrowserHistory(initialUrl);
            browserHistory.Visit("https://www.google.com");

            // Act
            string previousUrl = browserHistory.Back();

            // Assert
            Assert.Equal("https://www.example.com", previousUrl);
        }

        [Fact]
        public void Forward_GoesToNextPage()
        {
            // Arrange
            string initialUrl = "https://www.example.com";
            BrowserHistory browserHistory = new BrowserHistory(initialUrl);
            browserHistory.Visit("https://www.google.com");
            browserHistory.Back();

            // Act
            string nextUrl = browserHistory.Forward();

            // Assert
            Assert.Equal("https://www.google.com", nextUrl);
        }
    }
}
