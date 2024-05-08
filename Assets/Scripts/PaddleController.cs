using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for paddle controllers.
/// </summary>
/// TODO: This feels a little funny. It's only got one line of code! Maybe there is a different way to do this.
/// Feels a little overkill maybe? Something to think about or keep an eye on maybe.
public abstract class PaddleController : MonoBehaviour
{
    public abstract Vector3 Move();
}