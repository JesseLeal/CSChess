using System;
using ChessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess960Tests
{
    [TestClass]
    public class UnitTest1
    {
        public Board board = new Board();

        [TestMethod]
        public void BishopsAreOppositeColors()
        {
            Chess960BoardSetup();

            Cell[] bishops = new Cell[2];
            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                if (board[$"{(char)(i + 97)}1"].piece.IsBishop())
                {
                    bishops[index] = board[$"{(char)(i + 97)}1"];
                    index++;
                }
            }

            Assert.AreNotEqual(bishops[0].IsDark, bishops[1].IsDark);
        }

        [TestMethod]
        public void KingIsBetweenRooks()
        {
            Chess960BoardSetup();

            int[] rookPos = new int[2];
            int kingPos = -1;
            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                if (board[$"{(char)(i + 97)}1"].piece.IsRook())
                {
                    rookPos[index] = i;
                    index++;
                }
                else if (board[$"{(char)(i + 97)}1"].piece.IsKing())
                {
                    kingPos = i;
                }
            }

            Assert.IsTrue(rookPos[0] < kingPos);
            Assert.IsTrue(kingPos < rookPos[1]);
        }

        [TestMethod]
        public void PiecesAreOpposite()
        {
            Chess960BoardSetup();

            for (int i = 0; i < 8; i++)
            {
                Assert.AreEqual(board[$"{(char)(i + 97)}1"].piece.Type, board[$"{(char)(i + 97)}8"].piece.Type);
            }
        }

        [TestMethod]
        public void CorrectNumPieces()
        {
            Chess960BoardSetup();

            int kings = 0;
            int queens = 0;
            int bishops = 0;
            int knights = 0;
            int rooks = 0;
            int pawns = 0;
            int[] validRows = new int[] { 1, 2, 7, 8 };
            for (int x = 0; x < 8; x++)
            {
                foreach (int y in validRows)
                {
                    switch (board[$"{(char)(x + 97)}{y}"].piece.Type)
                    {
                        case Piece.PieceType.King:
                            kings++;
                            break;
                        case Piece.PieceType.Queen:
                            queens++;
                            break;
                        case Piece.PieceType.Bishop:
                            bishops++;
                            break;
                        case Piece.PieceType.Knight:
                            knights++;
                            break;
                        case Piece.PieceType.Rook:
                            rooks++;
                            break;
                        case Piece.PieceType.Pawn:
                            pawns++;
                            break;
                        default:
                            break;
                    }
                }
            }
                
            Assert.AreEqual(2, kings);
            Assert.AreEqual(2, queens);
            Assert.AreEqual(4, bishops);
            Assert.AreEqual(4, knights);
            Assert.AreEqual(4, rooks);
            Assert.AreEqual(16, pawns);
        }

        public void Chess960BoardSetup()
        {
            board.Init960();
        }
    }
}