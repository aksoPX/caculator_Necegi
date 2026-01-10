using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace caculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // Подписки из конструктора удалены, так как они есть в XAML (Clicked="...")
        }

        // Метод для валидации (проверки) ввода
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(ExperienceEntry.Text) ||
                string.IsNullOrWhiteSpace(RequiredExperienceEntry.Text) ||
                string.IsNullOrWhiteSpace(MultiplierEntry.Text))
            {
                DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
                return false;
            }

            if (!double.TryParse(ExperienceEntry.Text, out _) ||
                !double.TryParse(RequiredExperienceEntry.Text, out _) ||
                !double.TryParse(MultiplierEntry.Text, out double multiplier))
            {
                DisplayAlert("Ошибка", "Введите корректные числовые значения", "OK");
                return false;
            }

            if (multiplier <= 0)
            {
                DisplayAlert("Ошибка", "Множитель должен быть больше 0", "OK");
                return false;
            }

            return true;
        }

        // Красивое появление результата
        private async Task ShowResultWithAnimation(string message)
        {
            // Устанавливаем текст
            ResultLabel.Text = message;

            // Подготавливаем фрейм к анимации (делаем прозрачным и чуть смещаем вниз)
            ResultFrame.IsVisible = true;
            ResultFrame.Opacity = 0;
            ResultFrame.TranslationY = 20;

            // Запускаем одновременно появление и подъем вверх
            await Task.WhenAll(
                ResultFrame.FadeTo(1, 400, Easing.CubicOut),
                ResultFrame.TranslateTo(0, 0, 400, Easing.CubicOut)
            );
        }

        // Логика кнопки опыта
        private async void OnCalculateExpClicked(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                double currentExp = Convert.ToDouble(ExperienceEntry.Text);
                double requiredExp = Convert.ToDouble(RequiredExperienceEntry.Text);
                double multiplier = Convert.ToDouble(MultiplierEntry.Text);

                double a = requiredExp - currentExp;
                double b = a + (a * 24 / 100);
                double cleanExp = b / multiplier;
                int result = Convert.ToInt32(cleanExp);

                await ShowResultWithAnimation($"Чистый опыт до ранга: {result:N0}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        // Логика кнопки золота
        private async void OnCalculateGoldClicked(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                double currentExp = Convert.ToDouble(ExperienceEntry.Text);
                double requiredExp = Convert.ToDouble(RequiredExperienceEntry.Text);
                double multiplier = Convert.ToDouble(MultiplierEntry.Text);

                double a = requiredExp - currentExp;
                double b = a + (a * 24 / 100);
                double cleanExp = b / multiplier;
                double gold = cleanExp / 9700 * 5;
                int result = Convert.ToInt32(gold);

                await ShowResultWithAnimation($"Необходимое золото: {result:N0}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}