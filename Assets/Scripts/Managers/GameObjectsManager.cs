﻿using Components;
using Models;
using UnityEngine;
using Views;

namespace Managers
{
    public class GameObjectsManager : BaseMonoBehaviour
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private BoardView _boardViewPrefab;
        [SerializeField]
        private SquareView _squareViewPrefab;
        [SerializeField]
        private PieceView _pieceViewPrefab;

        private Canvas _canvasInstance;
        private BoardView _boardViewInstance;

        public override void Initialize()
        {
            if (_canvasInstance == null)
            {
                _canvasInstance = Instantiate(_canvasPrefab);
            }
            if (_boardViewInstance == null)
            {
                _boardViewInstance = CreateBoard();
                _boardViewInstance.gameObject.transform.SetParent(_canvasInstance.transform);
            }

            BoardViewInstanceOnRedrawCompleted();
            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();

            if (_boardViewInstance != null)
            {
                Destroy(_boardViewInstance.gameObject);
                _boardViewInstance = null;
            }
            if (_canvasInstance != null)
            {
                Destroy(_canvasInstance.gameObject);
                _canvasInstance = null;
            }
        }

        public override void Subscribe()
        {
            _boardViewInstance.RedrawCompleted += BoardViewInstanceOnRedrawCompleted;
        }
        public override void UnSubscribe()
        {
            if (_boardViewInstance != null)
            {
                _boardViewInstance.RedrawCompleted -= BoardViewInstanceOnRedrawCompleted;
            }
        }

        private void CenterBoardViewInstancePosition()
        {
            _boardViewInstance.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public PieceView CreatePiece(PieceModel pieceModel)
        {
            var pieceViewInstance = Instantiate(_pieceViewPrefab);
            pieceViewInstance.Initialize(pieceModel);
            return pieceViewInstance;
        }
        public SquareView CreateSquare(SquareModel squareModel)
        {
            var squareViewInstance = Instantiate(_squareViewPrefab);
            squareViewInstance.Initialize(squareModel);
            return squareViewInstance;
        }
        public BoardView CreateBoard()
        {
            var boardViewInstance = Instantiate(_boardViewPrefab);
            return boardViewInstance;
        }

        private void BoardViewInstanceOnRedrawCompleted()
        {
            CenterBoardViewInstancePosition();
        }
    }
}
