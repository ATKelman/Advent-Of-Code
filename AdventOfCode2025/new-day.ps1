param(
    [Parameter(Position=0)]
    [int]$DayNumber = 0
)

$ErrorActionPreference = "Stop"
$minDay = 1
$maxDay = 12

# Determine day number
if ($DayNumber -eq 0) {
    $existingDays = Get-ChildItem -Path "Days" -Filter "Day*.cs" -ErrorAction SilentlyContinue |
        ForEach-Object {
            if ($_.BaseName -match "Day(\d+)") {
                [int]$matches[1]
            }
        } | Sort-Object

    if ($existingDays) {
        $DayNumber = ($existingDays | Measure-Object -Maximum).Maximum + 1
    } else {
        $DayNumber = 1
    }

    Write-Host "Auto-detected next day: $DayNumber" -ForegroundColor Cyan
}

if ($DayNumber -lt $minDay -or $DayNumber -gt $maxDay) {
    Write-Host "Error: Day number must be between $minDay and $maxDay" -ForegroundColor Red
    exit 1
}

$dayFile = "Days\Day$DayNumber.cs"

if (Test-Path $dayFile) {
    Write-Host "Error: $dayFile already exists!" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path "Days")) {
    New-Item -ItemType Directory -Path "Days" | Out-Null
}

$template = @"
using Microsoft.Extensions.Configuration;

namespace AdventOfCode2025.Days;

public class Day$DayNumber (IConfiguration config)
    : DayBase(config)
{
    public override string SolvePart1()
    {
        // TODO: Implement Part 1
        return "Not implemented";
    }

    public override string SolvePart2()
    {
        // TODO: Implement Part 2
        return "Not implemented";
    }
}
"@

Set-Content -Path $dayFile -Value $template -Encoding UTF8

Write-Host "Created: $dayFile" -ForegroundColor Green


Write-Host ""
Write-Host "Day $DayNumber Setup!" -ForegroundColor Yellow
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  1. Input will be auto-created and downloaded on first run"
Write-Host "  2. Run with: dotnet run -- $DayNumber"
