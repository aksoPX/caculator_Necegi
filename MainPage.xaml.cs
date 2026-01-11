using System;
using Microsoft.Maui.Controls;

namespace caculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnRankSelected(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button.CommandParameter != null)
        {
            RequiredExperienceEntry.Text = button.CommandParameter.ToString();
            ResultFrame.IsVisible = false;
        }
    }

    // КНОПКА ОПЫТА (Тут всё без изменений, просто целое число)
    private async void OnCalculateExpClicked(object sender, EventArgs e)
    {
        double op = GetValue(ExperienceEntry.Text);
        double nado = GetValue(RequiredExperienceEntry.Text);
        double x = GetValue(MultiplierEntry.Text, 1);

        double a = nado - op;
        if (a <= 0) { ResultLabel.Text = "Цель достигнута!"; }
        else
        {
            double b = a + (a * 24.0 / 100.0);
            double chist = b / x;
            ResultLabel.Text = $"Нужно опыта: {Math.Ceiling(chist):N0}";
        }
        await ShowRes();
    }

    // КНОПКА ЗОЛОТА (С округлением до 0 или 5 в большую сторону)
    private async void OnCalculateGoldClicked(object sender, EventArgs e)
    {
        double op = GetValue(ExperienceEntry.Text);
        double nado = GetValue(RequiredExperienceEntry.Text);
        double x = GetValue(MultiplierEntry.Text, 1);

        double a = nado - op;
        if (a <= 0) { ResultLabel.Text = "Цель достигнута!"; }
        else
        {
            double b = a + (a * 24.0 / 100.0);
            double chist = b / x;
            double rawGold = (chist / 9700.0) * 5.0;

            // ОКРУГЛЕНИЕ ВВЕРХ ДО КРАТНОГО 5
            // Например: 1902 / 5 = 380.4 -> RoundUp = 381 -> 381 * 5 = 1905
            double goldRounded = Math.Ceiling(rawGold / 5.0) * 5.0;

            // Если получилось 0, но опыт нужен — ставим минимум 5
            if (goldRounded < 5 && rawGold > 0) goldRounded = 5;

            ResultLabel.Text = $"Нужно золота: {goldRounded:N0}";
        }
        await ShowRes();
    }

    private double GetValue(string text, double defaultValue = 0)
    {
        if (double.TryParse(text, out double result)) return result;
        return defaultValue;
    }

    private async Task ShowRes()
    {
        ResultFrame.IsVisible = true;
        ResultFrame.Opacity = 0;
        await ResultFrame.FadeTo(1, 400);
    }
}