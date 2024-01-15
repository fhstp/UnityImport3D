# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres
to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Upcoming

## 0.11.1

### Changed

- Project structure. All runtime related files are now inside
the Runtime folder. All Code files have been moved into a Code
subfolder.

## 0.10.1

### Added

- Sample-info in readme

## 0.10.0

### Added

- Import-tester sample
- Invisible importing
- Helper function to check if file import is supported

## 0.9.0

### Fixed

- Fbx-scaling issue

## 0.8.0

### Added

- Usage info in readme
- Import materials
  - Name
  - Albedo color
  - Opacity
  - Specular color
  - Emission
  - Smoothness/Glossiness

### Changed

- Config mutation syntax. Removed mutation methods.
  Use record `with` syntax instead

## 0.7.0

### Added

- Import node transforms

### Changed

- Bump Opt-unity to 3.4.0

## 0.6.2

### Added

- Configure scene parent-gameobject after import
- Option to import only meshes from file

## 0.6.1

### Added

- Configure Assimp post-process-steps for imports
- Configure scene-name

## 0.6.0

### Changed

- Bump target Unity version to 2021.3
- Remove assembly-validation warning as its no longer needed

## 0.5.1

### Fixed

- Readme claimed that win x32 was supported when that was not true

### Changed

- Assembly-definition file-name to match assembly name

## 0.5.0

### Added

- Android support (v7 and arm64)
- Win32 support

### Fixed

- Wrong term in readme

## 0.4.0

### Fixed

- Missing meta files

### Added

- Note about assembly validation in readme

## 0.3.0

### Fixed

- Missing shader in build

### Added

- Package keywords in manifest

### Fixed

- Missing 0.2.0 version header in changelog

## 0.2.0

### Added

- Root-node now has scene file-name as name

### Fixed

- Missing 0.1.0 version header in changelog

## 0.1.0

### Added

- Import textures
- Import basic meshes (without UVs or tangents)
- Import scenes (Nodes with meshes. No materials yet)