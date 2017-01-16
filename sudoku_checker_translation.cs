using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver_final
{
    class Program
    {

        public static bool checkPerBox(int[,,] sudoku3DList)
        {
            int nextRow;
            int iter3D;
            int itemzz;
            for (int box = 0; box < 9; box++) {
                for (int row = 0; row < 3; row++) {
                    for (int column = 0; column < 3; column++) {
                        iter3D = 0;

                        for (int item = 0; item < 9; item++) {
                            itemzz = item;
                            if ( (iter3D >= 3) && (iter3D < 6)) {
                                itemzz -= 3; nextRow = 1;
                            }
                            else if (iter3D >= 6) {
                                itemzz -= 6; nextRow = 2;
                            }
                            else { nextRow = 0;  }

                            if ( (column == itemzz) && (row == nextRow)) { }

                            else if (sudoku3DList[box, row, column] == sudoku3DList[box, nextRow, itemzz]) {
                                //Console.WriteLine("sudoku3DList[{0}][{1}][{2}] == sudoku3DList[{ 0}][{5}][{3}]", 
                                //box, row, column, item, iter3D, nextRow);
                                return false;
                            }
                            iter3D++;
                        }
                    }
                }
            }
            return true;
        } // checkPerBox

        public static bool checkPerRow(int[,] sudoku2DList) {
            for (int row = 0; row < 9; row++) {
                for (int column = 0; column < 9; column++) {
                    for (int item = 0; item < 9; item++) {
                        if (column == item) { }

                        else if (sudoku2DList[row,column] != sudoku2DList[row, item]) { /*pass*/ }

                        else if (sudoku2DList[row,column] == sudoku2DList[row, item]) { return false; }
                    }
                }
            }

            return true;
        } // checkPerRow

        public static bool checkPerCol(int[,] sudoku2DList) {
            for (int row = 0; row < 9; row++) {
                for (int column = 0; column < 9; column++) {
                    for (int item = 0; item < 9; item++) {
                        if (row == item) { }    // I wonder why In the .py version of this code, 
                                                // even if the code was col == item, the code still works lol, this is just weird

                        else if (sudoku2DList[column, row] != sudoku2DList[column, item]) { /*pass*/ }

                        else if (sudoku2DList[column, row] == sudoku2DList[column, item]) {
                            Console.WriteLine("{0}{1} == {0}{2}",column, row, item);
                            Console.WriteLine("{0}, {1}", column, item);
                            return false; }
                    }
                }
            }
            return true;
        } // checkPerCol

        public static void make3d(string sudoku_input, ref int[,,] sudoku_3dlist) {
            int iter = 0;
            int rowz;
            int boxzz;
            for (int row = 0; row < 9; row++) {
                rowz = row;
                if ((iter >= 27) && (iter < 54)) { rowz -= 3; }

                if (iter >= 54) { rowz -= 6; }

                for (int box = 0; box < 3; box++) {
                    boxzz = box;
                    if ((iter >= 27) && (iter < 54)) { boxzz += 3; }

                    if (iter >= 54) { boxzz += 6; }

                    for (int column = 0; column < 3; column++) {
                        // Console.WriteLine("box = {0}, row = {1}, col = {2}, iter = {3}", boxzz, rowz, column, iter);
                        sudoku_3dlist[boxzz, rowz, column] = sudoku_input[iter];
                        iter++;
                    }
                }
            } 
        } // make3d

        static void Main(string[] args)
        {
            bool lenCheck;
            int[,] sudoku_list = new int[9, 9];
            int[,,] sudoku_3dlist = new int[9, 3, 3]; // 9 box dimensions, 3 ea. for row and column

            Console.WriteLine("Enter sudoku puzzle: ");
            string sudoku_input = Console.ReadLine();

            if (sudoku_input.Length != 81) {
                lenCheck = false;
            }
            else {
                lenCheck = true;
            }

            // transform into 2D array
            // 649123875827465139135789264518934627276518493493672518752346981364891752981257346

            int ctr = 0;

            for (int row = 0; row < 9; row++) {
                for (int column = 0; column < 9; column++) {
                    sudoku_list[row, column] = sudoku_input[ctr];
                    ctr++;
                }
            }

            make3d(sudoku_input, ref sudoku_3dlist);

            bool perBoxTruthVal = checkPerBox(sudoku_3dlist);
            bool perColTruthVal = checkPerCol(sudoku_list);
            bool perRowTruthVal = checkPerRow(sudoku_list);

            Console.WriteLine("perBoxTruthVal = {0}", perBoxTruthVal);
            Console.WriteLine("perColTruthVal = {0}", perColTruthVal);
            Console.WriteLine("perRowTruthVal = {0}", perRowTruthVal);
            Console.WriteLine("========================\nSolution is '{0}'!!", (lenCheck && perBoxTruthVal &&
                                                                            perColTruthVal && perRowTruthVal));
        }
    } // class Program
}  // namespace
