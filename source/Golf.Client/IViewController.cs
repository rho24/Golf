using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Controls;
using Golf.Client.Views;
using Golf.Core;
using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Client
{
    public interface IViewController
    {
        void Initialize(Canvas canvas);
        IEnumerable<GolfBallView> Views { get; }
    }

    public class ViewController : IViewController
    {
        readonly IGameEngine _gameEngine;
        readonly ICollection<GolfBallView> _views = new List<GolfBallView>();
        Canvas _canvas;

        public ViewController(IGameEngine gameEngine) {
            _gameEngine = gameEngine;
            _gameEngine.Events.OfType<IGameObjectCreated<object>>().Subscribe(OnGameObjectCreated);
        }

        public IEnumerable<GolfBallView> Views {
            get { return _views; }
        }

        #region IViewController Members

        public void Initialize(Canvas canvas) {
            _canvas = canvas;
        }

        #endregion

        void OnGameObjectCreated(IGameObjectCreated<object> e) {
            if (e is GameObjectCreated<GolfBall>) {
                var view = new GolfBallView(((GameObjectCreated<GolfBall>) e).GameObject);
                _views.Add(view);
                _canvas.Children.Add(view);
            }
        }
    }
}