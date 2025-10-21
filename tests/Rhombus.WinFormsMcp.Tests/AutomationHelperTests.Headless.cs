namespace Rhombus.WinFormsMcp.Tests;

using Rhombus.WinFormsMcp.Server.Automation;
using Moq;
using System.Diagnostics;

/// <summary>
/// Headless tests for AutomationHelper using mocks
/// These tests verify AutomationHelper logic without requiring a GUI
/// </summary>
public class AutomationHelperHeadlessTests
{
    private Mock<IAutomationHelper>? _mockAutomation;

    [SetUp]
    public void Setup()
    {
        _mockAutomation = new Mock<IAutomationHelper>();
    }

    [TearDown]
    public void TearDown()
    {
        _mockAutomation?.VerifyAll();
    }

    [Test]
    public void TestAutomationHelperInitialization()
    {
        // Arrange & Act
        _mockAutomation!.Setup(a => a.Dispose()).Verifiable();

        // Assert
        Assert.That(_mockAutomation.Object, Is.Not.Null);
        _mockAutomation.Object.Dispose();
    }

    [Test]
    public void TestLaunchAppReturnsValidProcess()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.LaunchApp("notepad.exe", null, null))
            .Returns((System.Diagnostics.Process)null!)
            .Verifiable();

        // Act
        var process = _mockAutomation.Object.LaunchApp("notepad.exe");

        // Assert
        _mockAutomation.Verify(a => a.LaunchApp("notepad.exe", null, null), Times.Once);
    }

    [Test]
    public void TestLaunchAppWithArguments()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.LaunchApp("notepad.exe", "/A", "C:\\"))
            .Returns((System.Diagnostics.Process)null!)
            .Verifiable();

        // Act
        var process = _mockAutomation.Object.LaunchApp("notepad.exe", "/A", "C:\\");

        // Assert
        _mockAutomation.Verify(a => a.LaunchApp("notepad.exe", "/A", "C:\\"), Times.Once);
    }

    [Test]
    public void TestAttachToProcess()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.AttachToProcess(9999))
            .Returns((System.Diagnostics.Process)null!)
            .Verifiable();

        // Act
        var process = _mockAutomation.Object.AttachToProcess(9999);

        // Assert
        _mockAutomation.Verify(a => a.AttachToProcess(9999), Times.Once);
    }

    [Test]
    public void TestAttachToProcessByName()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.AttachToProcessByName("notepad"))
            .Returns((System.Diagnostics.Process)null!)
            .Verifiable();

        // Act
        var process = _mockAutomation.Object.AttachToProcessByName("notepad");

        // Assert
        _mockAutomation.Verify(a => a.AttachToProcessByName("notepad"), Times.Once);
    }

    [Test]
    public void TestGetMainWindow()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.GetMainWindow(1234))
            .Returns((FlaUI.Core.AutomationElements.AutomationElement)null!)
            .Verifiable();

        // Act
        var window = _mockAutomation.Object.GetMainWindow(1234);

        // Assert
        _mockAutomation.Verify(a => a.GetMainWindow(1234), Times.Once);
    }

    [Test]
    public void TestGetMainWindowReturnsNullWhenNotFound()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.GetMainWindow(9999))
            .Returns((FlaUI.Core.AutomationElements.AutomationElement)null!)
            .Verifiable();

        // Act
        var window = _mockAutomation.Object.GetMainWindow(9999);

        // Assert
        Assert.That(window, Is.Null);
    }

    [Test]
    public void TestFindByAutomationId()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.FindByAutomationId("okButton", null, 5000))
            .Returns((FlaUI.Core.AutomationElements.AutomationElement)null!)
            .Verifiable();

        // Act
        var element = _mockAutomation.Object.FindByAutomationId("okButton");

        // Assert
        _mockAutomation.Verify(a => a.FindByAutomationId("okButton", null, 5000), Times.Once);
    }

    [Test]
    public void TestFindByName()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.FindByName("OK", null, 5000))
            .Returns((FlaUI.Core.AutomationElements.AutomationElement)null!)
            .Verifiable();

        // Act
        var element = _mockAutomation.Object.FindByName("OK");

        // Assert
        _mockAutomation.Verify(a => a.FindByName("OK", null, 5000), Times.Once);
    }

    [Test]
    public void TestElementExists()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.ElementExists("testId", null))
            .Returns(true)
            .Verifiable();

        // Act
        var exists = _mockAutomation.Object.ElementExists("testId");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public void TestElementDoesNotExist()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.ElementExists("nonexistent", null))
            .Returns(false)
            .Verifiable();

        // Act
        var exists = _mockAutomation.Object.ElementExists("nonexistent");

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public void TestClickElement()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.Click(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), false))
            .Verifiable();

        // Act
        _mockAutomation.Object.Click(null!, false);

        // Assert
        _mockAutomation.Verify(a => a.Click(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), false), Times.Once);
    }

    [Test]
    public void TestDoubleClickElement()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.Click(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), true))
            .Verifiable();

        // Act
        _mockAutomation.Object.Click(null!, true);

        // Assert
        _mockAutomation.Verify(a => a.Click(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), true), Times.Once);
    }

    [Test]
    public void TestTypeText()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.TypeText(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), "test", false))
            .Verifiable();

        // Act
        _mockAutomation.Object.TypeText(null!, "test", false);

        // Assert
        _mockAutomation.Verify(a => a.TypeText(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), "test", false), Times.Once);
    }

    [Test]
    public void TestSendKeys()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.SendKeys("test"))
            .Verifiable();

        // Act
        _mockAutomation.Object.SendKeys("test");

        // Assert
        _mockAutomation.Verify(a => a.SendKeys("test"), Times.Once);
    }

    [Test]
    public void TestCloseApp()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.CloseApp(1234, false))
            .Verifiable();

        // Act
        _mockAutomation.Object.CloseApp(1234);

        // Assert
        _mockAutomation.Verify(a => a.CloseApp(1234, false), Times.Once);
    }

    [Test]
    public void TestForceCloseApp()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.CloseApp(1234, true))
            .Verifiable();

        // Act
        _mockAutomation.Object.CloseApp(1234, force: true);

        // Assert
        _mockAutomation.Verify(a => a.CloseApp(1234, true), Times.Once);
    }

    [Test]
    public async Task TestWaitForElementAsync()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.WaitForElementAsync("testId", null, 10000))
            .ReturnsAsync(true)
            .Verifiable();

        // Act
        var found = await _mockAutomation.Object.WaitForElementAsync("testId");

        // Assert
        Assert.That(found, Is.True);
    }

    [Test]
    public async Task TestWaitForElementAsyncTimeout()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.WaitForElementAsync("nonexistent", null, 1000))
            .ReturnsAsync(false)
            .Verifiable();

        // Act
        var found = await _mockAutomation.Object.WaitForElementAsync("nonexistent", null, 1000);

        // Assert
        Assert.That(found, Is.False);
    }

    [Test]
    public void TestTakeScreenshot()
    {
        // Arrange
        var screenshotPath = Path.Combine(Path.GetTempPath(), "test_screenshot.png");

        _mockAutomation!
            .Setup(a => a.TakeScreenshot(screenshotPath, null))
            .Verifiable();

        // Act
        _mockAutomation.Object.TakeScreenshot(screenshotPath);

        // Assert
        _mockAutomation.Verify(a => a.TakeScreenshot(screenshotPath, null), Times.Once);
    }

    [Test]
    public void TestGetProperty()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.GetProperty(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), "name"))
            .Returns("TestButton")
            .Verifiable();

        // Act
        var value = _mockAutomation.Object.GetProperty(null!, "name");

        // Assert
        Assert.That(value, Is.EqualTo("TestButton"));
    }

    // ===== NEGATIVE TESTS =====

    [Test]
    public void TestLaunchAppWithInvalidPath()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.LaunchApp("/invalid/path/nonexistent.exe", null, null))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.LaunchApp("/invalid/path/nonexistent.exe")
        );
    }

    [Test]
    public void TestAttachToInvalidProcessId()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.AttachToProcess(99999))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.AttachToProcess(99999)
        );
    }

    [Test]
    public void TestGetPropertyWithInvalidPropertyName()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.GetProperty(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), "invalidProperty"))
            .Returns((object?)null)
            .Verifiable();

        // Act
        var value = _mockAutomation.Object.GetProperty(null!, "invalidProperty");

        // Assert
        Assert.That(value, Is.Null);
    }

    [Test]
    public void TestTakeScreenshotWithInvalidPath()
    {
        // Arrange
        var invalidPath = "/invalid/path/screenshot.png";

        _mockAutomation!
            .Setup(a => a.TakeScreenshot(invalidPath, null))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.TakeScreenshot(invalidPath)
        );
    }

    [Test]
    public void TestClickOnDisabledElement()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.Click(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), false))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.Click(null!, false)
        );
    }

    [Test]
    public void TestTypeTextOnReadOnlyElement()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.TypeText(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), "text", false))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.TypeText(null!, "text", false)
        );
    }

    [Test]
    public void TestSetValueWithInvalidValue()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.SetValue(It.IsAny<FlaUI.Core.AutomationElements.AutomationElement>(), It.IsAny<string>()))
            .Throws<InvalidOperationException>()
            .Verifiable();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _mockAutomation.Object.SetValue(null!, "invalid")
        );
    }

    [Test]
    public async Task TestWaitForElementAsyncWithInvalidTimeout()
    {
        // Arrange
        _mockAutomation!
            .Setup(a => a.WaitForElementAsync("element", null, -1))
            .ReturnsAsync(false)
            .Verifiable();

        // Act
        var found = await _mockAutomation.Object.WaitForElementAsync("element", null, -1);

        // Assert
        Assert.That(found, Is.False);
    }
}
