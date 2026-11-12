# InputSanitizer

Check user input for injection signs and clean it up. Backend only, C# console app.

Detects SQL injection, XSS, path traversal, long input, and empty input, then returns a risk level and a sanitized version of the input.

## Features

- SQL injection detection for `' or`, `or 1=1`, `union select`, `drop table`
- XSS detection for `<script`, `onerror=`, `javascript:`
- Path traversal detection for `../` and `..\`
- Long input check, over 100 chars
- Empty input check
- Sanitizer that removes `<`, `>`, `'`, `"`, `;`, `--`
- Risk levels LOW, MEDIUM, HIGH
- Case insensitive checks with `ToLower`
- No printing inside helpers, only `Main` prints

## Project Structure

- `Program.cs` - Main app, all 4 methods
- `InputSanitizer.csproj` - Project file, .NET 8.0

## How to Run

```bash
dotnet run -- "hello123"
dotnet run -- "admin' OR '1'='1"
dotnet run -- "<script>alert(1)</script>"
dotnet run -- "../../etc/passwd"
dotnet run
