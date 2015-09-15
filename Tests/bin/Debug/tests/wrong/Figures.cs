using System;

namespace Figures {

    class Rectangle {
        private int x, y, width, height;
        public Rectangle(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
    }

    class Circle {
        private int x, y, radius;
        public Circle(int x, int y, int radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
        public int getX() { return x; }
        public int getY() { return y; }
        public int getRadius() { return radius; }
    }

}

