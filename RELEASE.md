## AQA Prompt Engineering Demo - Release Notes

### Version 0.1.0
- Initial public demo with NUnit + Selenium tests:
  - Zero-shot (`SmokeZeroShot.cs`)
  - Few-shot (`FewShotTests.cs`)
  - Advanced (`AdvancedTests.cs`)
- Stabilized selectors and waits for `books.toscrape.com`.
- Added teardown screenshot capture on failures.

### Requirements
- .NET SDK 8+
- Google Chrome (stable)

### Running
- `dotnet test -c Release`

### Known issues / notes
- The demo site may not always expose a success banner or mini-basket element consistently; assertions rely on robust selectors.
- If Chrome updates, Selenium Manager resolves ChromeDriver automatically; no manual driver install is needed.

