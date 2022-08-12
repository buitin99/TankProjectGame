using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFigure
{
    public void OnCollisionEnter(Collision collision);
    public void Death();
    public void TakeDamage(int damge);
}
