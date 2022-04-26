# Divine

**Divine** is the command line interpreter for [LSLib](https://github.com/Norbyte/lslib).

LSLib allows you to manipulate assets for the following games:

- Divinity: Original Sin
- Divinity: Original Sin Enhanced Edition
- Divinity: Original Sin 2
- Divinity: Original Sin 2 Definitive Edition
- Baldur's Gate 3

Divine provides command line access to the following actions:

- Creating, extracting, and listing files within PAK packages
- Creating and extracting LSV savegame packages
- Converting LSB, LSF, LSX, LSJ resource files
- Importing and exporting meshes and animations (conversion from/to GR2 format)

Divine does not support editing story (OSI) databases.

# Arguments

```text
required arguments:

  -s, --source                     Source file path or directory

  -a, --action                     Action to execute
    Allowed Values:
      create-package, list-package, extract-single-file, extract-package,
      extract-packages, convert-model, convert-models, convert-resource,
      convert-resources

optional arguments:

  -l, --loglevel                   Log output verbosity
    Default Value:
      info
    Allowed Values:
      off, fatal, error, warn, info, debug, trace, all

  -g, --game                       Target game when generating output
    Default Value:
      autodetect
    Allowed Values:
      dos, dosee, dos2, dos2de, bg3, autodetect

  -d, --destination                Destination file path or directory

  -f, --packaged-path              File to extract from package

  -i, --input-format               Input format for batch operations
    Allowed Values:
      dae, gr2, lsv, lsj, lsx, lsb, lsf

  -o, --output-format              Output format for batch operations
    Allowed Values:
      dae, gr2, lsv, lsj, lsx, lsb, lsf

  -c, --compression-method         Compression method
    Default Value:
      lz4hc
    Allowed Values:
      zlib, zlibfast, lz4, lz4hc, none

  -e, --gr2-options                Extra options for GR2/DAE conversion
    Allowed Values:
      export-normals, export-tangents, export-uvs, export-colors,
      deduplicate-vertices, deduplicate-uvs, recalculate-normals,
      recalculate-tangents, recalculate-iwt, flip-uvs, ignore-uv-nan,
      y-up-skeletons, force-legacy-version, compact-tris, build-dummy-skeleton,
      apply-basis-transforms, x-flip-skeletons, x-flip-meshes, conform,
      conform-copy

  -x, --expression                 Glob expression for extract and list actions

  --conform-path                   Conform to original path

  --use-package-name               Use package name for destination folder

  --use-regex                      Use Regular Expressions for expression type
```

# Syntax

## Conversion

```shell
# Convert Model
divine -a convert-model -g {game} -s {source_file} -d {target_file} -e {values}

# Convert Resource
divine -a convert-resource -g {game} -s {source_file} -d {target_file}
```

## Packaging

```shell
# List Package
divine -a list-package -g {game} -s {source_file} 

# Create Package
divine -a create-package -g {game} -s {source_dir} -d {target_file}

# Extract Package
divine -a extract-package -g {game} -s {source_file} -d {target_dir}

# Extract Single File (from Package)
divine -a extract-single-file -g {game} -s {source_file} -d {target_file}
```

## Batch

```shell
# Batch Convert Models
divine -a convert-models -g {game} -s {source_dir} -d {target_dir} -e {values} -i {input_format} -o {output_format}

# Batch Convert Resources
divine -a convert-resources -g {game} -s {source_dir} -d {target_dir} -i {input_format} -o {output_format}

# Batch Extract Packages
divine -a extract-packages -g {game} -s {source_dir} -d {target_dir}
```

# Drag-and-Drop Support

This fork of Divine offers the following behaviors for extraction:

- When a package is dropped on the executable, Divine will extract that package into the current working directory. The file name will be used as the name of the target directory.
- When a folder is dropped on the executable, Divine will extract all packages in that directory into the current working directory. Each file name will be used as the name of each target directory.

The command executed is equivalent to:

```shell
divine {file_or_directory_path}
```

The path can be absolute or relative. If a relative path is passed, Divine will treat that path as relative to the current working directory. In addition, the game will be detected from the package.

For example, this command extracts all packages in the current working directory.

```shell
divine .
```

This command extracts the specified package into the current working directory.

```shell
divine "LetThereBeTooltips_c03acb8a-03b9-4c79-84ff-3784e97774da.pak"
```

# Building from Source Code

To build the project, you'll need the following dependencies:

- [LSLib](https://github.com/Norbyte/lslib) (Divine links `LSLib.dll` for fast builds, but you can link the project, if desired.)
- [CommandLineArgumentsParser](https://www.nuget.org/packages/CommandLineArgumentsParser/)
