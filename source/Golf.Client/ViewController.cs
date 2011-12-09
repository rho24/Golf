using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Controls;
using Golf.Client.ViewModels;
using Golf.Client.Views;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Client
{
    public class ViewController : IViewController
    {
        readonly IGameEngine _gameEngine;
        readonly ICollection<GolfBallView> _views = new List<GolfBallView>();
        Canvas _canvas;

        public ViewController(IGameEngine gameEngine) {
            _gameEngine = gameEngine;
            _gameEngine.Events.OfType<IGameObjectCreated<object>>().Subscribe(OnGameObjectCreated);
        }

        #region IViewController Members

        public IEnumerable<GolfBallView> Views {
            get { return _views; }
        }

        public void Initialize(Canvas canvas) {
            _canvas = canvas;
        }

        #endregion

        void OnGameObjectCreated(IGameObjectCreated<object> e) {
            if (e is GameObjectCreated<GolfBall>) {
                var ball = ((GameObjectCreated<GolfBall>) e).GameObject;
                var view = new GolfBallView(ball);
                _views.Add(view);
                _canvas.Children.Add(view);

                var shotControlView = new ShotControlView(_canvas) {
                                                                       DataContext =
                                                                           new ShotControlModel {
                                                                                                    PlayersBall = ball,
                                                                                                    PowerX = 214.0,
                                                                                                    PowerY = 100.0
                                                                                                }
                                                                   };
                _canvas.Children.Add(shotControlView);
            }
        }
    }
}