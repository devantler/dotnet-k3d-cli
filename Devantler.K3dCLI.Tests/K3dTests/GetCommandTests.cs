using System.Runtime.InteropServices;

namespace Devantler.K3dCLI.Tests.K3dTests;

/// <summary>
/// Tests for the <see cref="K3d.GetCommand(PlatformID?, Architecture?, string?)"/> method.
/// </summary>
public class GetCommandTests
{
  /// <summary>
  /// Test to verify that the command returns the correct binary for MacOS on x64 architecture.
  /// </summary>
  [Theory]
  [InlineData(PlatformID.Unix, Architecture.X64, "osx-x64", "k3d-osx-x64")]
  [InlineData(PlatformID.Unix, Architecture.Arm64, "osx-arm64", "k3d-osx-arm64")]
  [InlineData(PlatformID.Unix, Architecture.X64, "linux-x64", "k3d-linux-x64")]
  [InlineData(PlatformID.Unix, Architecture.Arm64, "linux-arm64", "k3d-linux-arm64")]
  [InlineData(PlatformID.Win32NT, Architecture.X64, "win-x64", "k3d-win-x64.exe")]
  public void GetCommand_ShouldReturnOSXx64Binary(PlatformID platformID, Architecture architecture, string runtimeIdentifier, string expectedBinary)
  {
    // Act
    string actualBinary = Path.GetFileName(K3d.GetCommand(platformID, architecture, runtimeIdentifier).TargetFilePath);

    // Assert
    Assert.Equal(expectedBinary, actualBinary);
  }

  /// <summary>
  /// Test to verify that the command returns a <see cref="PlatformNotSupportedException"/> when the platform is not supported.
  /// </summary>
  [Fact]
  public void GetCommand_GivenInvaldiPlatform_ShouldThrowPlatformNotSupportedException()
  {
    // Arrange
    var platformID = PlatformID.Other;
    var architecture = Architecture.Wasm;
    string runtimeIdentifier = "wasm";

    // Act
    void Act() => K3d.GetCommand(platformID, architecture, runtimeIdentifier);

    // Assert
    _ = Assert.Throws<PlatformNotSupportedException>(Act);
  }
}