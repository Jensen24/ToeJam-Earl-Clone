using System;

public class CameraSystem
{
    public Vector2 Position;
    public CameraSystem(Vector2 startPos)
    {
        Position = startPos;
    }

    public void Trail(Rectangle target, Vector2 screenSize)
    {
        Position = new Vector2(

            -target.X + (screenSize.X / 2 - target.Width / 2),
            -target.Y + (screenSize.Y / 2 - target.Height / 2)

            );
    }

    public Matrix GetViewMatrix()
    {
        return Matrix.CreateTranslation(new Vector3(Position, 0f));
    }
}
