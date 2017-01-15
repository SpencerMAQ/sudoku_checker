using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// program doesn't work yet
// translation from my working Python sudoku_checker

namespace sudoku_solver_final
{
    class Program
    {

        public static bool checkPerBox(int[,,] sudoku3DList)
        {
            int nextRow;
            int iter3D;
            for (int box = 0; box < 9; box++) {
                for (int row = 0; row < 3; row++) {
                    for (int column = 0; column < 3; column++) {
                        iter3D = 0;

                        for (int item = 0; item < 9; item++) {
                            if ( (iter3D >= 3) && (iter3D < 6)) {
                                item -= 3; nextRow = 1;
                            }
                            else if (iter3D >= 6) {
                                item -= 6; nextRow = 2;
                            }
                            else { nextRow = 0;  }

                            if ( (column == item) && (row == nextRow)) { }

                            else if (sudoku3DList[box, row, column] == sudoku3DList[box, nextRow, item]) {
                                //Console.WriteLine("sudoku3DList[{0}][{1}][{2}] == sudoku3DList[{ 0}][{5}][{3}]", box, row, column, item, iter3D, nextRow);
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
        }

        public static bool checkPerCol(int[,] sudoku2DList) {
            for (int row = 0; row < 9; row++) {
                for (int column = 0; column < 9; column++) {
                    for (int item = 0; item < 9; item++) {
                        if (column == item) { }

                        else if (sudoku2DList[column, row] != sudoku2DList[column, item]) { /*pass*/ }

                        else if (sudoku2DList[column, row] == sudoku2DList[column, item]) { return false; }
                    }
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            bool lenCheck;
            int[,] sudoku_list = new int[9, 9];
            int[,,] sudoku_3dlist = new int[3, 3, 3];
            int iter = 0;


            Console.WriteLine("Enter sudoku puzzle: ");
            string sudoku_input = Console.ReadLine();

            if(sudoku_input.Length != 81) {
                lenCheck = false;
            }
            else {
                lenCheck = true;
                // Console.WriteLine(sudoku_input.Length);
            }

            // transform into 2D array
            

            // 649123875827465139135789264518934627276518493493672518752346981364891752981257346
            // Console.WriteLine("hi1");
            int ctr = 0;
            // Console.WriteLine("hi2");
            for (int row = 0; row < 9; row++) {
                Console.WriteLine("hi3");
                for (int column = 0; column < 9; column++) {
                    sudoku_list[row, column] = sudoku_input[ctr];
                    ctr++;
                }
            }
            Console.WriteLine("hi4");

            // 649123875827465139135789264518934627276518493493672518752346981364891752981257346
            // it can't even reach this for loop.. I wonder why...
            for (int row3 = 0; row3 > 9; row3++) {
                Console.WriteLine("hi666666666");
                if ((iter >= 27) && (iter < 54)) {
                    row3 -= 3;
                }
                else if (iter >= 54) {
                    row3 -= 6;
                }
                for (int box = 0; box > 3; box++) {
                    if ((iter >= 27) && (iter < 54)) {
                        box += 3;
                    }
                    else if (iter >= 54) {
                        box += 6;
                    }

                    for (int column = 0; column > 3; column++) {
                        Console.WriteLine(sudoku_3dlist[box,row3,column]);
                        sudoku_3dlist[box, row3, column] = sudoku_input[iter];
                        iter++;
                    }
                }
            }
            // 649123875827465139135789264518934627276518493493672518752346981364891752981257346
            // Console.WriteLine("hi5");

            //Console.WriteLine(sudoku_3dlist[1, 2, 2]);

            bool perBoxTruthVal = checkPerBox(sudoku_3dlist);
            bool perColTruthVal = checkPerCol(sudoku_list);
            bool perRowTruthVal = checkPerRow(sudoku_list);

            Console.WriteLine("perBoxTruthVal = {0}", perBoxTruthVal);
            Console.WriteLine("perColTruthVal = {0}", perColTruthVal);
            Console.WriteLine("perRowTruthVal = {0}", perRowTruthVal);
            Console.WriteLine("========================\nSolution is '{0}'!!", (lenCheck && perBoxTruthVal &&
                                                                            perColTruthVal && perRowTruthVal));
        }
    } // class
}  // namespace
