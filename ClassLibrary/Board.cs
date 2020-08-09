/***************************************************************
 * File: Board.cs
 * Created By: Justin Grindal		Date: 27 June, 2013
 * Description: The main chess board. Board contain the chess cell
 * which will contain the chess pieces. Board also contains the methods
 * to get and set the user moves.
 ***************************************************************/

using System;
using System.Collections;
using System.Xml;

namespace ChessLibrary
{
	/// <summary>
	/// he main chess board. Board contain the chess cell
	/// which will contain the chess pieces. Board also contains the methods
	/// to get and set the user moves.
	/// </summary>
    [Serializable]
	public class Board
	{
		private Side m_WhiteSide, m_BlackSide;	// Chess board site object 
		private Cells m_cells;	// collection of cells in the board

		public Board()
		{
            m_WhiteSide = new Side(Side.SideType.White);	// Makde white side
            m_BlackSide = new Side(Side.SideType.Black);	// Makde white side

			m_cells = new Cells();					// Initialize the chess cells collection
		}

		// Initialize the chess board and place piece on thier initial positions
		public void Init()
		{
			m_cells.Clear();		// Remove any existing chess cells

			// Build the 64 chess board cells
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					m_cells.Add(new Cell(row,col));	// Initialize and add the new chess cell
				}

			// Now setup the board for black side
			m_cells["a1"].piece = new Piece(Piece.PieceType.Rook,m_BlackSide);
			m_cells["h1"].piece = new Piece(Piece.PieceType.Rook,m_BlackSide);
			m_cells["b1"].piece = new Piece(Piece.PieceType.Knight,m_BlackSide);
			m_cells["g1"].piece = new Piece(Piece.PieceType.Knight,m_BlackSide);
			m_cells["c1"].piece = new Piece(Piece.PieceType.Bishop,m_BlackSide);
			m_cells["f1"].piece = new Piece(Piece.PieceType.Bishop,m_BlackSide);
			m_cells["e1"].piece = new Piece(Piece.PieceType.King,m_BlackSide);
			m_cells["d1"].piece = new Piece(Piece.PieceType.Queen,m_BlackSide);
			for (int col=1; col<=8; col++)
				m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn,m_BlackSide);

			// Now setup the board for white side
			m_cells["a8"].piece = new Piece(Piece.PieceType.Rook,m_WhiteSide);
			m_cells["h8"].piece = new Piece(Piece.PieceType.Rook,m_WhiteSide);
			m_cells["b8"].piece = new Piece(Piece.PieceType.Knight,m_WhiteSide);
			m_cells["g8"].piece = new Piece(Piece.PieceType.Knight,m_WhiteSide);
			m_cells["c8"].piece = new Piece(Piece.PieceType.Bishop,m_WhiteSide);
			m_cells["f8"].piece = new Piece(Piece.PieceType.Bishop,m_WhiteSide);
			m_cells["e8"].piece = new Piece(Piece.PieceType.King,m_WhiteSide);
			m_cells["d8"].piece = new Piece(Piece.PieceType.Queen,m_WhiteSide);
			for (int col=1; col<=8; col++)
				m_cells[7, col].piece = new Piece(Piece.PieceType.Pawn,m_WhiteSide);
		}

        public void Init960()
        {
            m_cells.Clear();        // Remove any existing chess cells

            // Build the 64 chess board cells
            for (int row = 1; row <= 8; row++)
                for (int col = 1; col <= 8; col++)
                {
                    m_cells.Add(new Cell(row, col));    // Initialize and add the new chess cell
                }

            // Perform random setup for the black side, mirroring on the white side as it goes
            Random rand = new Random();
            bool[] takenSpots = new bool[8];
            int index;

            //then pick 1,3,5,7 and place a bishop and cross the number
            index = (rand.Next(4) * 2) + 97;
            m_cells[$"{(char)index}1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells[$"{(char)index}8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
            takenSpots[index - 97] = true;

            //then pick 2,4,6,8 and place a bishop and cross the number
            index = (rand.Next(4) * 2) + 98;
            m_cells[$"{(char)index}1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells[$"{(char)index}8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
            takenSpots[index - 97] = true;

            //then pick a number 1-8 [and make sure the number is ok] for queen, place and cross
            do
            {
                index = rand.Next(8) + 97;
            } while (takenSpots[index - 97]);
            m_cells[$"{(char)index}1"].piece = new Piece(Piece.PieceType.Queen, m_BlackSide);
            m_cells[$"{(char)index}8"].piece = new Piece(Piece.PieceType.Queen, m_WhiteSide);
            takenSpots[index - 97] = true;

            //then pick a number 1-8 [and make sure the number is ok] for knight, place and cross x2
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    index = rand.Next(8) + 97;
                } while (takenSpots[index - 97]);
                m_cells[$"{(char)index}1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
                m_cells[$"{(char)index}8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
                takenSpots[index - 97] = true;
            }

            //then iterate through the numbers, put a rook in the first, king in the second, rook in the third
            int found = 0;
            for (int i = 0; i < takenSpots.Length; i++)
            {
                if (!takenSpots[i])
                {
                    if (found == 1)
                    {
                        //that means that this one is the king
                        m_cells[$"{(char)(i + 97)}1"].piece = new Piece(Piece.PieceType.King, m_BlackSide);
                        m_cells[$"{(char)(i + 97)}8"].piece = new Piece(Piece.PieceType.King, m_WhiteSide);
                        takenSpots[i] = true;
                    }
                    else
                    {
                        //that means that this one is a rook
                        m_cells[$"{(char)(i + 97)}1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
                        m_cells[$"{(char)(i + 97)}8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
                        takenSpots[i] = true;
                    }
                    found++;
                }
            }

            for (int col = 1; col <= 8; col++)
                m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide);

            for (int col = 1; col <= 8; col++)
                m_cells[7, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide);
        }

		// get the new item by rew and column
		public Cell this[int row, int col]
		{
			get
			{
				return m_cells[row, col];
			}
		}

		// get the new item by string location
		public Cell this[string strloc]
		{
			get
			{
				return m_cells[strloc];	
			}
		}

		// get the chess cell by given cell
		public Cell this[Cell cellobj]
		{
			get
			{
				return m_cells[cellobj.ToString()];	
			}
		}

        /// <summary>
        /// Serialize the Game object as XML String
        /// </summary>
        /// <returns>XML containing the Game object state XML</returns>
        public XmlNode XmlSerialize(XmlDocument xmlDoc)
        {
            XmlElement xmlBoard = xmlDoc.CreateElement("Board");

            // Append game state attributes
            xmlBoard.AppendChild(m_WhiteSide.XmlSerialize(xmlDoc));
            xmlBoard.AppendChild(m_BlackSide.XmlSerialize(xmlDoc));

            xmlBoard.AppendChild(m_cells.XmlSerialize(xmlDoc));

            // Return this as String
            return xmlBoard;
        }

        /// <summary>
        /// DeSerialize the Board object from XML String
        /// </summary>
        /// <returns>XML containing the Board object state XML</returns>
        public void XmlDeserialize(XmlNode xmlBoard)
        {
            // Deserialize the Sides XML
            XmlNode side = XMLHelper.GetFirstNodeByName(xmlBoard, "Side");

            // Deserialize the XML nodes
            m_WhiteSide.XmlDeserialize(side);
            m_BlackSide.XmlDeserialize(side.NextSibling);

            // Deserialize the Cells
            XmlNode xmlCells = XMLHelper.GetFirstNodeByName(xmlBoard, "Cells");
            m_cells.XmlDeserialize(xmlCells);
        }

		// get all the cell locations on the chess board
		public ArrayList GetAllCells()
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// get all the cell containg pieces of given side
        public ArrayList GetSideCell(Side.SideType PlayerSide)
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					// check and add the current type cell
					if (this[row,col].piece!=null && !this[row,col].IsEmpty() && this[row,col].piece.Side.type == PlayerSide)
						CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// Returns the cell on the top of the given cell
		public Cell TopCell(Cell cell)
		{
			return this[cell.row-1, cell.col];
		}

		// Returns the cell on the left of the given cell
		public Cell LeftCell(Cell cell)
		{
			return this[cell.row, cell.col-1];
		}

		// Returns the cell on the right of the given cell
		public Cell RightCell(Cell cell)
		{
			return this[cell.row, cell.col+1];
		}

		// Returns the cell on the bottom of the given cell
		public Cell BottomCell(Cell cell)
		{
			return this[cell.row+1, cell.col];
		}

		// Returns the cell on the top-left of the current cell
		public Cell TopLeftCell(Cell cell)
		{
			return this[cell.row-1, cell.col-1];
		}

		// Returns the cell on the top-right of the current cell
		public Cell TopRightCell(Cell cell)
		{
			return this[cell.row-1, cell.col+1];
		}

		// Returns the cell on the bottom-left of the current cell
		public Cell BottomLeftCell(Cell cell)
		{
			return this[cell.row+1, cell.col-1];
		}

		// Returns the cell on the bottom-right of the current cell
		public Cell BottomRightCell(Cell cell)
		{
			return this[cell.row+1, cell.col+1];
		}
	}
}
