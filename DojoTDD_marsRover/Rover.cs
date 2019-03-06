using System;
using System.Collections.Generic;

namespace DojoTDD_marsRover
{
    public class Rover
    {
        private int x = 0;
        private int y = 0;
        private char direction = 'N';

        private int minGrid = 0, maxGrid = 9;

        private Dictionary<char, NextDirection> nextDirections = new Dictionary<char, NextDirection>()
        {
            { 'N', new NextDirection('O', 'E') },
            { 'E', new NextDirection('N', 'S') },
            { 'S', new NextDirection('E', 'O') },
            { 'O', new NextDirection('S', 'N') }
        };

        private ISet<Tuple<int, int>> obstacles = new HashSet<Tuple<int, int>>();
        private bool hasObstacleInTheWay;

        public string sendCommand(string command)
        {
            hasObstacleInTheWay = false;
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
            string str = $"{x}:{y}:{direction}";
            if (hasObstacleInTheWay)
            {
                str = "O:" + str;
            }
            return str;
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
            var previousCoordinates = (x, y);
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

            if (isInObstacle())
            {
                hasObstacleInTheWay = true;
                (x, y) = previousCoordinates;
            }
        }

        private bool isInObstacle()
        {
            foreach (var obstacle in obstacles)
            {
                if (x == obstacle.Item1 && y == obstacle.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddObstacle(int x, int y)
        {
            var newObstacle = Tuple.Create(x, y);
            obstacles.Add(newObstacle);
        }

        public bool HasObstacle(int x, int y)
        {
            var key = Tuple.Create(x, y);
            return obstacles.Contains(key);
        }

        public void RemoveObstacle(int x, int y)
        {
            var key = Tuple.Create(x, y);
            obstacles.Remove(key);
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
