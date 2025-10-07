using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

public class InteractionManager
{
    private ToeJam _player;
    private madDentist _enemy;
    private Present _present;
    public InteractionManager(ToeJam player, madDentist enemy, Present present)
    {
        _player = player;
        _enemy = enemy;
        _present = present;
    }
    public void Update()
    {
        EnemyInteractions();
        ItemPickup();
    }

    private void EnemyInteractions()
    {
        Vector2 playerPos = _player.PlayerPosition;
        Vector2 enemyPos = _enemy.DentistPosition;
        Vector2 toEnemy = enemyPos - playerPos;
        float distance = toEnemy.Length();

        Vector2 playerDir = InputManager.Direction;
        Vector2 toEnemyDir = Vector2.Normalize(toEnemy);
        float dot = Vector2.Dot(playerDir, toEnemyDir);
        float cross = playerDir.X * toEnemyDir.Y - playerDir.Y * toEnemyDir.X;

        if (distance < 200f)
        {
            if (dot > 0.7f)
                System.Diagnostics.Debug.WriteLine("ToeJam is facing an Enemy!");
            if (cross > 0)
                System.Diagnostics.Debug.WriteLine("An Enemy is to the Left!");
            else if (cross < 0)
                System.Diagnostics.Debug.WriteLine("An Enemy is to the Right!");
        }
    }
    private void ItemPickup()
    {
        Vector2 playerPos = _player.PlayerPosition;
        Vector2 presentPos = _present.presentPosition;
        float distance = Vector2.Distance(playerPos, presentPos);

        if (distance < 20f)
        {
            System.Diagnostics.Debug.WriteLine("Item Acquired!");
        }
    }
}
