# AQA Prompt Engineering Demo

Демонстрація того, як різні техніки промпт‑інжинірингу (Zero‑shot, Few‑shot, Chain‑of‑thought, Tree‑of‑thought, Critic/Refactor)
впливають на якість і стабільність автоматизованих UI‑тестів для демо‑сайту [Books to Scrape](http://books.toscrape.com/).

## Стек
C# + Selenium WebDriver + NUnit

## Структура
- **Pages/** — Page Object класи (`HomePage`, `CategoryPage`, `ProductPage`, `ProductPage.Extensions`)
- **Tests/** — приклади тестів:
  - `SmokeZeroShot.cs` — Zero‑shot скелет
  - `FewShotTests.cs` — Few‑shot із POM та очікуваннями
  - `AdvancedTests.cs` — Chain‑of‑thought, Tree‑of‑thought, Critic/Refactor
- **Utils/** — `TestLogger`, `Retry`, `ScreenshotTaker`

## Запуск
1. Встановіть .NET SDK 8+ і Google Chrome (стабільна версія).
2. Встановлення залежностей вже описане у `aqa-prompt-engineering-demo-full.csproj` — додаткових кроків не потрібно.
3. Запуск тестів:
   - `dotnet test -c Release`
4. Примітки:
   - Selenium Manager автоматично підбере відповідний ChromeDriver для встановленого Chrome.
   - Тести відкривають сайт `http://books.toscrape.com/`. Для стабільності уникнено крихких селекторів.

