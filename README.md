# Import3D

An asynchronous runtime 3D-model importer for Unity

Check out the changelog [here](./CHANGELOG.md).

## Features

Import3D supports most common 3D-model formats
as it uses [Assimp](https://github.com/assimp/assimp)
internally.

Imports a scene's node-structure and geometry.
Materials and textures are not imported yet.

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
    "org.nuget.dev.comradevanti.nothing",
    "org.nuget.system.buffers",
    "org.nuget.system.collections.immutable",
    "org.nuget.system.memory",
    "org.nuget.system.numerics.vectors",
    "org.nuget.system.runtime.compilerservices.unsafe"
  ]
}
```

## Compatibility

Developed for/with Unity 2020.3