using UnityEngine;
public interface IGun 
{
  bool ISFIRE { get; }
  void Fire(bool isFire);
}
public interface IplayerController
{
    float ISMOVE { get; }
    float ISTURN { get; }
    void Turn(float isTurn);
    void Move(float isMove);
}
public interface IBlow
{
    bool ISBLOW { get; }
    void BlowEnd(bool isBlow);
    GameObject BLOW(GameObject blowEffect,Vector3 position,bool isBlow);

}
public interface Iswip
{
    bool ISSWIP { get; }
    void Swip(bool isSwip);
}
