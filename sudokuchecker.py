import sys
sudoku_input = input("Enter Sudoku puzzle: ")

if len(sudoku_input) != 81:
    lenCheck = False
    sys.exit()

else:
    lenCheck = True

# why is it that I can't instantiate multidimensional arrays in Python?
# I've seen some code of initializations using for loops, but I'd like it this nonetheless, for visualization
# changes made from blah-blah BRANCH
sudoku_list = [
                [1,1,1,1,1,1,1,1,1],
                [2,2,2,2,2,2,2,2,2],
                [3,3,3,3,3,3,3,3,3],
                [4,4,4,4,4,4,4,4,4],
                [5,5,5,5,5,5,5,5,5],
                [6,6,6,6,6,6,6,6,6],
                [7,7,7,7,7,7,7,7,7],
                [8,8,8,8,8,8,8,8,8],
                [9,9,9,9,9,9,9,9,9]
              ]

sudoku3DList = [   # not representative of actual formation
                [
                    [1,1,1],
                    [1,1,1],
                    [1,1,1]
                ],
                [[2,2,2], [2,2,2], [2,2,2]],
                [[3,3,3], [3,3,3], [3,3,3]],
                [[4,4,4], [4,4,4], [4,4,4]],
                [[5,5,5], [5,5,5], [5,5,5]],
                [[6,6,6], [6,6,6], [6,6,6]],
                [[7,7,7], [7,7,7], [7,7,7]],
                [[8,8,8], [8,8,8], [8,8,8]],
                [[9,9,9], [9,9,9], [9,9,9]]
              ]

def make2d():
    ctr = 0  # assign to 2D list
    for row in range(9):
        for column in range(9):
            sudoku_list[row][column] = sudoku_input[ctr]
            ctr += 1

make2d()


def make3d():                # transform the list into a 3D list
    iter = 0
    for row in range(9):
        if 54 > iter >= 27:  # and iter < 54:
            row -= 3
        if iter >= 54:
            row -= 6

        for box in range(3):
            if 54 > iter >= 27:  # and iter < 54:
                box += 3
            if iter >= 54:
                box += 6

            for column in range(3):
                sudoku3DList[box][row][column] = sudoku_input[iter]
                iter += 1

make3d()


def checkPerBox():  # check that no dupe per 3x3 box
    """
    It might not be necessary to transform it into a 3D array,
    but it should be more convenient this way just in case it would have its GUI
    """
    for box in range(9):                    # box as 3rd dimension
        for row in range(3):                # left-hand side row to compare
            for column in range(3):         # left-hand side column to compare
                iter3D = 0                  # gets reset each iteration of column (i.e. every 3) and completion of item
                                                                                                        # loop
                for item in range(9):       # each of the 9 items to compare to
                    if 6 > iter3D >= 3:     # and iter3D < 6:
                        item -= 3           # since item in range(9) goes from 0 from 8, we must subtract...
                        nextRow = 1             # ...3 if the iteration3D counter is b/n 3 and 6, or subtract 3...
                                                # ...if the inter3D counter >= 6 (i.e. 6-6 = 0, 7-6 = 1, 8-6 = 2)
                    elif iter3D >= 6:
                        item -= 6
                        nextRow = 2         # simply go to the next row every 3 iterations

                    else:                   # i.e. iter3D < 3
                        nextRow = 0

                    # ###
                    if column == item and row == nextRow:   # skip comparing value to itself
                        pass

                    elif sudoku3DList[box][row][column] == sudoku3DList[box][nextRow][item]:
                        #print("iter3d: {4}| sudoku_list[{0}][{1}][{2}] == sudoku_list[{0}][{5}][{3}]".format
                                                                            #(box, row, column,item,iter3D,nextRow))
                        return False

                    iter3D += 1
    return True


perBoxTruthVal = checkPerBox()


truthValueRows = False
for row in range(9):            # check by iterating through rows
    for column in range(9):
        for item in range(9):
            if column == item:  # skip comparing value to itself
                pass

            elif sudoku_list[row][column] != sudoku_list[row][item]:
                truthValueRows = True

            elif sudoku_list[row][column] == sudoku_list[row][item]:
                truthValueRows = False
                break

            else:
                print("error")


truthValueColumns = False
for row in range(9):            # check by iterating through rows
    for column in range(9):
        for item in range(9):
            if row == item:     # if column == item:  # skip checking itself
                pass

            elif sudoku_list[column][row] != sudoku_list[column][item]:
                truthValueColumns = True

            elif sudoku_list[column][row] == sudoku_list[column][item]:
                truthValueColumns = False
                break

            else:
                print("error")


print("perBoxTruthVal    = {}".format(perBoxTruthVal))
print("truthValueRows    = {}".format(truthValueRows))
print("truthValueColumns = {}".format(truthValueColumns))
print("========================\nSolution is '{}'!!".format(perBoxTruthVal and truthValueColumns and truthValueRows and lenCheck))
