# Draughts
###### en/ru readme file
Draughts console game. This project is laboratory work.
In order to go, you need to specify the coordinates of the beginning and end.

## Preview
##### View in rider console
![a screenshot of the preview console application](https://i.imgur.com/x0uB1Tw.png?1)
##### View in original console
![a screenshot of the preview original console application](https://i.imgur.com/ey4TbLZ.png?1)

## Rules 
Pool checkers, also called "American pool checkers", is a variant of draughts, mainly played in the mid-Atlantic and southeastern United States and in Puerto Rico.
As in the related game English draughts (also known as American checkers or straight checkers), the game is played on an 8x8 board with the double corner (corner without a checker) to each player's right. The opponent playing the dark pieces will start the game by making the first move. One difference from the rules of English checkers is that a piece may capture both forward and backward. A player must capture an opponent's checker when possible (both forward and backward), but if two possibilities exist, the player may choose the sequence (even if one sequence has more jumps). The pieces are not removed until all jumps are completed and the player's hand is removed from his piece. A player may not capture an opponent more than one time. A player may not capture his own pieces. Another difference is that kings are flying kings. A king can jump any number of squares forward and backward. A king can make right turns after a jump and continue along another path after successfully taking an opponent. A king must also make all the possible jumps during a sequence. If the condition arises that one player has three kings and the other has just one king, the player who has the three kings must win within thirteen moves (even if the fourteenth move is a capture).

## Правила 
Для того, чтобы сходить нужно указать координаты начала хода и окончания
Пул чекерс (пул) - популярный в США, на Ямайке и в Африке вариант игры. Правила аналогичны международным шашкам с некоторыми отличиями:
игра ведётся на доске 8×8, по 12 шашек с каждой стороны
первый ход делают чёрные
в пул чекерс не требуется бить максимальное количество шашек.
На Ямайке играют по белым полям. Для объединения всех разновидностей пула был предложен вариант Unified Pool, в нём используется доска и нотация как в русские и бразильские шашки, эта разновидность была официально включена в стандарт PDN.

## Basic functions
Function name           | function Description
------------------------|----------------------
turn                    | The main function
printInfoAboutTurn      | Outputs additional information about who goes to the console
print                   | Prints the Board in the console
intToChar               | Converts the numeric value of shape (a draught, a king or empty) in the cell to symbol
setToPositionStructType | Converts entered string into a private structure, which subsequently runs the program
move                    | Making the move from point to point
isDiagonal              | Is there a diagonal between these points
makeDraughtToKing       | Makes all draughts to kings, that reached the edge of the playing area 
canTurn                 | Checks the player's ability to turn
canDraughtEatOneMove    | Can a draught eat when you move from point to point
generateTurnInCombo     | Allows to know that player can to play if player eat any draught 
draughtEat              | Eating the enemy
draughtTurn             | Move draughts
canTurnInKingCombo      | Can the king eat another figures after eating
kingEat                 | King eating of the enemy
simpleMove              | A Simple move of the kings from point to point
kingTurn                | Kings turn
isYourTurn              | The figure should belong to turn player 


## Основные функции
Название функции        | Описание функции
------------------------|----------------------
turn                    | Непосредственно ход игрока, главная функция
printInfoAboutTurn      | Выводит дополнительную информацию о том, кто ходит в консоль
print                   | Выводит игровое поле в консоль
intToChar               | Преобразует цифровое значение вида фигуры (шашка, дамка или пусто) в клетке в символ
setToPositionStructType | Преобразует введенную строку в приватную структуру, с которой в последствии работает программа
move                    | Совершает ход из точки в точку
isDiagonal              | Существует ли диагональ между этими точками
makeDraughtToKing       | Делает все шашки, достигшие края игровой зоны дамками
canTurn                 | Проверяет возможность хода игрока
canDraughtEatOneMove    | Сможет ли шашка съесть при ходе из точки в точку
generateTurnInCombo     | Позволяет узнать, может ли игрок съесть какую-либо шашку 
draughtEat              | Функция поедания противника
draughtTurn             | Ход шашки
canTurnInKingCombo      | Может ли дамка съесть еще противника, после съедения
kingEat                 | Поедание дамкой противника
simpleMove              | Простое перемещение дамки из точки в точку
kingTurn                | Ход дамки
isYourTurn              | Проверяет, является ли фигура собственностью ходящего
