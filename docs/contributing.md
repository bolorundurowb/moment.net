# Contributing

Contributions are welcome.

## Add a language

Relative time and the default calendar labels are translated for the cultures listed in the [guide](guide.md). To add another language:

1. Create a new `Strings.[language-code].resx` file in `src/moment.net/I18n/`.
2. Copy all string keys from `Strings.resx` and translate the values.
3. No `.csproj` changes are needed. .NET picks up satellite resource files by convention.

## Report bugs

Open an issue on GitHub.

## Pull requests

Submit pull requests for bug fixes or features, and include tests.

## Running tests

```bash
dotnet test
```

Release history is in [CHANGELOG.md](../CHANGELOG.md).
