using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using NLog;
using UiAutoTests.Helpers;
using FlaUI.Core.Definitions;

namespace UiAutoTests.Extensions
{
    public static class MenuExtensions
    {
        private static LoggerHelper _loggerHelper = new();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Раскрываем меню и его пункты, кроме последнего во вложении
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void ExpandMenuItem(this MenuItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();

            if (!menuItem.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Пункт меню {menuItem.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info($"Expand menu item: {menuItem.Name}");
            menuItem.Expand();
            _logger.Info("Menu item Expanded");
        }

        /// <summary>
        /// Кликает по последнему пункту меню во вложении
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static void ClickMenuItem(this MenuItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();

            if (!menuItem.WaitUntilEnabled(timeoutMs))
            {
                throw new TimeoutException($"Пункт меню {menuItem.AutomationId} не стал активным за {timeoutMs}мс");
            }

            _logger.Info($"Expand menu item: {menuItem.Name}");
            menuItem.Click();
            _logger.Info("Menu item Expanded");
        }

        /// <summary>
        /// Проверяет, активен ли пункт меню
        /// </summary>
        public static bool IsMenuItemEnabled(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            var state = menuItem.IsEnabled;
            _logger.Info($"[{menuItem.AutomationId}] is [{state}]");
            return state;
        }

        /// <summary>
        /// Проверяет, видим ли пункт меню
        /// </summary>
        public static bool IsMenuItemVisible(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            var isVisible = !menuItem.IsOffscreen;
            _logger.Info($"[{menuItem.AutomationId}] IsVisible - [{isVisible}]");
            return isVisible;
        }

        /// <summary>
        /// Ожидает, пока пункт меню не станет активным
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilEnabled(this MenuItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            
            var result = Retry.WhileFalse(
                () => menuItem.IsEnabled,
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{menuItem.AutomationId}] Wait until enabled result - [{result}]");
            return result;
        }

        /// <summary>
        /// Получает текст пункта меню
        /// </summary>
        public static string GetMenuItemText(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            var text = menuItem.Name;
            _logger.Info($"[{menuItem.AutomationId}] Text - [{text}]");
            return text;
        }

        /// <summary>
        /// Проверяет, имеет ли пункт меню дочерние элементы
        /// </summary>
        public static bool HasChildMenuItems(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            
            var hasChildMenuItems = menuItem.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem)).Any();
            _logger.Info($"[{menuItem.AutomationId}] Has child menu items - [{hasChildMenuItems}]");
            return hasChildMenuItems;
        }

        /// <summary>
        /// Получает все дочерние пункты меню
        /// </summary>
        public static IEnumerable<MenuItem> GetChildMenuItems(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            
            var items = menuItem.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem))
                              .Select(e => e.AsMenuItem());
            
            _logger.Info($"[{menuItem.AutomationId}] Child menu items count - [{items.Count()}]");
            return items;
        }

        /// <summary>
        /// Проверяет, открыто ли меню (есть ли видимые дочерние элементы)
        /// </summary>
        public static bool IsMenuOpen(this MenuItem automationElement)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();

            var isOpen = menuItem.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem))
                               .Any(item => !item.IsOffscreen);
            
            _logger.Info($"[{menuItem.AutomationId}] Menu is open - [{isOpen}]");
            return isOpen;
        }

        /// <summary>
        /// Ожидает, пока меню не будет открыто
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilMenuOpen(this MenuItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            
            var result = Retry.WhileFalse(
                () => {
                    var children = menuItem.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem));
                    return children.Any(item => !item.IsOffscreen);
                },
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{menuItem.AutomationId}] Wait until menu open result - [{result}]");
            return result;
        }

        /// <summary>
        /// Ожидает, пока меню не будет закрыто
        /// </summary>
        /// <param name="timeoutMs">Таймаут ожидания в миллисекундах</param>
        public static bool WaitUntilMenuClosed(this MenuItem automationElement, int timeoutMs = 5000)
        {
            _loggerHelper.LogEnteringTheMethod();
            var menuItem = automationElement.EnsureMenuItem();
            
            var result = Retry.WhileFalse(
                () => {
                    var children = menuItem.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem));
                    return !children.Any(item => !item.IsOffscreen);
                },
                TimeSpan.FromMilliseconds(timeoutMs)).Success;
                
            _logger.Info($"[{menuItem.AutomationId}] Wait until menu closed result - [{result}]");
            return result;
        }
    }
} 