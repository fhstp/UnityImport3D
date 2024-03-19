# Import3D

[![openupm](https://img.shields.io/npm/v/at.ac.fhstp.import3d?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/at.ac.fhstp.import3d/)

An asynchronous runtime 3D-model importer for Unity

Check out the changelog [here](./CHANGELOG.md).

**⚠️ Development is paused ⚠️**  
No new features will be added or bugs fixed unless requested through an issue.  
If you wish to fork this repository and continue the work, you are very welcome
to do so.

## Features

Import3D supports most common 3D-model formats
as it uses [Assimp](https://github.com/assimp/assimp)
internally.

Imports a scene's node-structure and geometry.
Materials and textures are not imported yet.

## Usage

Import functions are located on the static `Import` class.

```csharp
// using At.Ac.FhStp.Import3D

// Import a single file asynchronously
var importedGameObject = await Import.SingleAsync(filePath);
```

### Config

Some properties of the import process can be configured using
a `ImportConfig` object, which can be passed as a second argument
to all import functions. Refer to the `ImportConfig` XML-docs to
learn more.

```csharp
var config = ImportConfig.Default;
_ = await Import.SingleAsync(filePath, config);
```

## Targets

Import3D uses the C-library [Assimp](https://github.com/assimp/assimp)
internally. Because of this, not all target devices are supported
out of the box as binaries for these platforms have to built and included
in the package.

Currently Import3D supports the following platforms:

- Windows x64
- Android v7 / arm64

## Installation

Best way to install is via [OpenUPM](https://openupm.com/)
using `openupm add at.ac.fhstp.import3d`.

You can also install manually as a git-dependency from
`https://github.com/fhstp/UnityImport3D.git`. Make sure to add
the following scoped-registry if you choose to do so:

```json
{
  "name": "package.openupm.com",
  "url": "https://package.openupm.com",
  "scopes": [
    "com.openupm",
    "dev.comradevanti.opt-unity",
    "dev.comradevanti.rect-constraints",
    "org.nuget.comradevanti.csharptools.opt",
    "org.nuget.dev.comradevanti.nothing",
    "org.nuget.system.buffers",
    "org.nuget.system.collections.immutable",
    "org.nuget.system.memory",
    "org.nuget.system.numerics.vectors",
    "org.nuget.system.runtime.compilerservices.unsafe"
  ]
}
```

## Samples

The package also includes a sample to show how the import process can be started.
Have a look at `/Samples~/ImportTester`.

## Roadmap

- Texture import
- Better error-handling
- Optimizations
- More build targets

## Compatibility

Developed for/with Unity 2021.3
