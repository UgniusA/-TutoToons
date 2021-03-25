﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void Damage(int damage);

    IEnumerator DamageOverTime(int damage, float tickTime, float duration);

    void DestroyObject();
}
