using System;
using System.Collections.Generic;

namespace DojoTDD_marsRover
{
    public class Rover
    {
        private int x = 0;
        private int y = 0;
        private char direction = 'N';
        private bool obstacle = false;

        private int minGrid = 0, maxGrid = 9;

        public List<(int x, int y)> obstacles = new List<(int x, int y)>();

        private Dictionary<char, NextDirection> nextDirections = new Dictionary<char, NextDirection>()
        {
            { 'N', new NextDirection('O', 'E') },
            { 'E', new NextDirection('N', 'S') },
            { 'S', new NextDirection('E', 'O') },
            { 'O', new NextDirection('S', 'N') }
        };

        public string sendCommand(string command)
        {
            foreach (var move in command.ToUpper())
            {
                if (move == 'F')
                {
                    moveForward();
                }
                else
                {
                    direction = move == 'R' ? nextDirections[direction].Right : nextDirections[direction].Left;
                }
            }

            return toResponse();
        }

        private string toResponse()
        {
            return obstacle ? $"O:{x}:{y}:{direction}" : $"{x}:{y}:{direction}";
        }

        private int getVariableInGrid(int value)
        {
            if (value < minGrid)
                return maxGrid;

            if (value > maxGrid)
                return minGrid;

            return value;
        }

        private void moveForward()
        {
            var previousX = x;
            var previousY = y;

            switch (direction)
            {
                case 'N':
                    y = getVariableInGrid(y + 1);
                    break;

                case 'S':
                    y = getVariableInGrid(y - 1);
                    break;

                case 'E':
                    x = getVariableInGrid(x + 1);
                    break;

                case 'O':
                    x = getVariableInGrid(x - 1);
                    break;
            }

            if (obstacles.Exists(t => t.x == x && t.y == y))
            {
                obstacle = true;
                x = previousX;
                y = previousY;
            }
            else
            {
                obstacle = false;
            }
        }
    }

    class NextDirection
    {
        public char Left;
        public char Right;

        public NextDirection(char left, char right)
        {
            Left = left;
            Right = right;
        }
    }
}
