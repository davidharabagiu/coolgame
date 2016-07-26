﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coolgame.Systems
{
    public static class UIManager
    {
        private static List<Button> buttons = new List<Button>();
        private static List<UIWindow> windows = new List<UIWindow>();
        private static List<UIElement> elements = new List<UIElement>();

        private static Texture2D spaceCashTexture;
        private static Texture2D crosshair;
        private static bool displayCrosshair;

        private static int windowNumber = 0;
        private static bool pauseMenuOpen = false;
        public static bool PauseMenuOpen
        {
            get { return pauseMenuOpen; }
        }
        private static bool upgradeMenuOpen = false;
        public static bool UpgradeMenuOpen
        {
            get { return upgradeMenuOpen; }
        }

        //message variables
        private static SpriteFont messageFont;
        private static bool showMessage = false;
        private static string messageText;
        private static float messageDuration;
        private static float messageTimer;
        private static float fadeDuration = 500;
        private static Color messageColor = Color.White;

        private static SpriteFont gameFont;


        public static void LoadContent(ContentManager Content)
        {
            crosshair = Content.Load<Texture2D>("crosshair");
            spaceCashTexture = Content.Load<Texture2D>("spaceCash");
            messageFont = Content.Load<SpriteFont>("messageFont");
            gameFont = Content.Load<SpriteFont>("gameFont");
        }

        public static void DisplayMessage(string text)
        {
            showMessage = true;
            messageText = text;
            messageTimer = 0;
            messageColor = new Color(messageColor, 0);

            messageDuration = 3000;
        }

        public static void DisplayMessage(string text, float duration)
        {
            showMessage = true;
            messageText = text;
            messageTimer = 0;
            messageColor = new Color(messageColor, 0);

            messageDuration = duration;
        }

        public static void TogglePauseMenu()
        {
            if(pauseMenuOpen)
            {
                if(!upgradeMenuOpen)
                {
                    GameManager.State = GameState.Game;
                }
                pauseMenuOpen = false;
            }
            else
            {
                pauseMenuOpen = true;
                GameManager.State = GameState.Paused;
            }
        }

        public static void ToggleUpgradeMenu()
        {
            if (upgradeMenuOpen)
            {
                upgradeMenuOpen = false;
                GameManager.State = GameState.Game;
            }
            else
            {
                upgradeMenuOpen = true;
                GameManager.State = GameState.Paused;
            }
        }

        public static void AddElement(UIElement element)
        {
            elements.Add(element);
        }
        public static void AddElement(Button button)
        {
            buttons.Add(button);
        }
        public static void AddElement(UIWindow window)
        {
            windows.Add(window);
            windowNumber++;
        }

        public static void SetCrosshairDisplay(Game game, bool value)
        {
            displayCrosshair = value;
            game.IsMouseVisible = !value;
        }

        public static void Update(Game game, float deltaTime)
        {
            //Message display
            if (showMessage)
            {
                messageTimer += deltaTime;

                //Disable message
                if (messageTimer >= messageDuration)
                {
                    messageTimer = 0;
                    showMessage = false;
                }

                //Fade In
                if (messageTimer <= fadeDuration)
                {
                    messageColor = new Color(messageColor, messageColor.A + (int)(deltaTime / (fadeDuration / 255)));
                }

                //Fade Out
                if (messageTimer >= messageDuration - fadeDuration)
                {
                    messageColor = new Color(messageColor, messageColor.A - (int)(deltaTime / (fadeDuration / 255)));
                }
            }

            //Update menus
            for (int i = 0; i < windowNumber; i++)
            {
                windows[i].Update();
            }

            //Button events
            for (int i = 0; i < windowNumber; i++)
            {
                switch (i)
                {
                    //Upgrade Menu
                    case 0:
                        {
                            for (int j = 0; j < windows[i].GetButtons().Count; j++)
                            {
                                if (windows[i].GetButtons()[j].Pressed && upgradeMenuOpen)
                                {
                                    switch (j)
                                    {
                                        case 0:
                                            {
                                                GameManager.laser_damage++;
                                                break;
                                            }
                                        case 1:
                                            {
                                                GameManager.laser_speed++;
                                                break;
                                            }
                                        case 2:
                                            {
                                                GameManager.laser_spread++;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                }
                            }
                            break;
                        }
                    //Pause Menu
                    case 1:
                        {
                            for (int j = 0; j < windows[i].GetButtons().Count; j++)
                            {
                                if (windows[i].GetButtons()[j].Pressed && pauseMenuOpen)
                                    switch (j)
                                    {
                                        //Resume button
                                        case 0:
                                            {
                                                TogglePauseMenu();
                                                break;
                                            }
                                        //Restart Button
                                        case 1:
                                            {
                                                GameManager.Restart();
                                                TogglePauseMenu();
                                                break;
                                            }
                                        //Exit Button
                                        case 2:
                                            {
                                                game.Exit();
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            if (GameManager.State == GameState.StartMenu)
                            {
                                for (int j = 0; j < windows[i].GetButtons().Count; j++)
                                {
                                    if (windows[i].GetButtons()[j].Pressed)
                                        switch (j)
                                        {
                                            //Start
                                            case 0:
                                                {
                                                    GameManager.State = GameState.Game;
                                                    break;
                                                }
                                            //About
                                            case 1:
                                                {
                                                    //Show about section
                                                    break;
                                                }
                                            //Load
                                            case 2:
                                                {
                                                    //Load Game
                                                    break;
                                                }
                                            //Exit
                                            case 3:
                                                {
                                                    game.Exit();
                                                    break;
                                                }
                                            default:
                                                {
                                                    break;
                                                }
                                        }
                                }

                            }
                            break;
                        }
                    default:
                        {
                            Debug.Log("Invalid Menu Case");
                            break;
                        }
                }

            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spaceCashTexture, new Vector2(Game.GAME_WIDTH - 150, 33), Color.White);
            spriteBatch.DrawString(gameFont, "" + GameManager.SpaceCash, new Vector2(Game.GAME_WIDTH - 100, 40), Color.White);

            //Message Drawing
            if (showMessage)
            {
                spriteBatch.DrawString(
                    messageFont,
                    messageText, 
                    new Vector2(
                        Game.GAME_WIDTH/2 - messageFont.MeasureString(messageText).X/2, 
                        Game.GAME_HEIGHT/2 - messageFont.MeasureString(messageText).Y/2 - 150), 
                    messageColor);
            }


            //Menu Drawing
            for (int i = 0; i < windowNumber; i++)
            {
                switch (i)
                {
                    //Upgrade Menu
                    case 0:
                        {
                            if (upgradeMenuOpen)
                            {
                                windows[i].Draw(spriteBatch);
                            }
                            break;
                        }
                    //Pause Menu
                    case 1:
                        {
                            if (pauseMenuOpen)
                            {
                                windows[i].Draw(spriteBatch);
                            }
                            break;
                        }
                    case 2:
                        {
                            if(GameManager.State == GameState.StartMenu)
                            {
                                windows[i].Draw(spriteBatch);
                            }
                            break;
                        }
                    default:
                        {
                            Debug.Log("Invalid Menu Case");
                            break;
                        }
                }

            }

            for (int i = 1; i < elements.Count(); i++)
            {
                    elements[i].Draw(spriteBatch);
            }

            if(displayCrosshair)
            {
                spriteBatch.Draw(
                    crosshair,
                    new Vector2(
                        InputManager.MouseX - crosshair.Width / 2,
                        InputManager.MouseY - crosshair.Height / 2),
                    null,
                    null,
                    null,
                    0f,
                    new Vector2(1, 1),
                    Color.White,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
