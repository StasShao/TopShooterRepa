using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//============================================================================================================
public class PoolObjectNS
{
    private ObjetcPool _objPool;
    private List<PoolPrefabScript> _prefabList;
    private PoolPrefabScript _prefab;
    private int _poolCount;
    private int _maxpoolCount;
    private bool _autoExpand;
    public PoolObjectNS(ObjetcPool objPool, PoolPrefabScript prefab, List<PoolPrefabScript> prefabList, int poolCount, int maxpoolCount, bool autoExpand)
    {
        _autoExpand = autoExpand;
        _maxpoolCount = maxpoolCount;
        _prefabList = prefabList;
        _objPool = objPool;
        _prefab = prefab;
        _poolCount = poolCount;
        CreatePool();
    }
    private void CreatePool()
    {
        for (int i = 0; i < _poolCount; i++)
        {
            _objPool.CreatePool(_prefab);
        }
    }

    private bool TryGetFreeElement(out PoolPrefabScript element)
    {
        foreach (PoolPrefabScript item in _prefabList)
        {
            if(!item.gameObject.activeInHierarchy)
            {
                element = item;
                element.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }
    public PoolPrefabScript GetFreeElement()
    {
        if(TryGetFreeElement(out var element))
        {
            return element;
        }
        if (_prefabList.Count < _maxpoolCount && _autoExpand)
        {
            _objPool.CreatePool(_prefab);
            if (TryGetFreeElement(out element))
            {
                return element;
            }
        }
        throw new System.Exception("The pool is over");
    }
    public Rigidbody RbPool()
    {
        if(TryGetFreeElement(out var element))
        {
            return element._rb;
        }
        if(_prefabList.Count < _maxpoolCount && _autoExpand)
        {
            _objPool.CreatePool(_prefab);
            if (TryGetFreeElement(out element))
            {
                return element._rb;
            }
        }
        throw new System.Exception("The pool is over");
    }
    
   
}
//============================================================================================================
public class PlayerControllerNS
{
    public void Tick()
    {
        InterfaceListener();
        InputListener();
    }
    private void InterfaceListener()
    {
        if(DataBase.IGunData.ISFIRE)
        {
            DataBase.IGunData.Fire(false);
        }
    }
    private void InputListener()
    {
        if(Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
        {
            DataBase.IGunData.Fire(true);
        }
        DataBase.IplayerControllerData.Move(Input.GetAxis("Vertical"));
        DataBase.IplayerControllerData.Turn(Input.GetAxis("Horizontal"));
    }
}
//============================================================================================================
public class HitCollisionsNS
{
    private Blowing _blowing;
   
    public HitCollisionsNS(Blowing blowing)
    {
        _blowing = blowing;
        DataBase.IblowData = _blowing.GetComponent<IBlow>();
    }
    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == DataBase.PlayerData)
        {
            foreach (var item in DataBase.DestroyEffectMeshRenderData)
            {
                item.material = DataBase.PlayerMatData;
            }
            
            DataBase.EnemyPointsData++;
            DataBase.IblowData.BLOW(DataBase.BlowGoData, col.contacts[0].point,true);
            DataBase.BlowGoData.SetActive(true);
            DataBase.timer = 0.0f;
            _blowing.gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            DataBase.DestroyEffectData.transform.position = new Vector3(DataBase.PlayerData.transform.position.x, 2, DataBase.PlayerData.transform.position.z); 
            DataBase.DestroyEffectData.SetActive(true);
        }
        if (col.gameObject == DataBase.EnemyData)
        {
            foreach (var item in DataBase.DestroyEffectMeshRenderData)
            {
                item.material = DataBase.EnemyMatData;
            }
            DataBase.PointsData++;
            DataBase.IblowData.BLOW(DataBase.BlowGoData, col.contacts[0].point,true);
            DataBase.BlowGoData.SetActive(true);
            DataBase.timer = 0.0f;
            _blowing.gameObject.SetActive(false);
           col.gameObject.SetActive(false);
            DataBase.DestroyEffectData.transform.position = new Vector3(DataBase.EnemyData.transform.position.x, 2, DataBase.EnemyData.transform.position.z);
            DataBase.DestroyEffectData.SetActive(true);
        }
    }
  
}
//============================================================================================================
   public class AINS
   {
    private float _moveForce;
    private EnemyAI _enemyAI;
    private Rigidbody _rb;
    private Transform _pivot;
    private Transform _raySartPoint;
    private Transform _enemyHeadTransform;
    private Transform _pointer;
    private Transform _ricochetPointer;
    private Transform _randomPointer;
    private float _rayDistance;
    private Color _rayColor;
    private LayerMask _rayLayer;
    private bool _isDetected;
    private Animator _searcherAnim;
    private NavMeshAgent _navMeshA;
    private float _curentNavSpeed;
    private float _curentRayDistance;
    private float _curentRayDistance2;
    private float _rayDist2 = 100;
    private float angle2;
    private float _offset;
    private Vector3 dirr;
    private Vector3 dirr2;
    private Vector3 _RicochetTargetPosition;
    private Vector3 _randomPos;
    private bool _isRicochetDetect;
    private float tmer;
    private float _randomRangeX;
    private float _randomRangeZ;
    private float _fireTimer;
    private float _swipTimer;

    public AINS(EnemyAI enemyAI, Transform pivot, Transform raySartPoint, float rayDistance, Color rayColor, LayerMask rayLayer, Animator searcherAnim, NavMeshAgent navMeshA, Rigidbody rb, Transform enemyHeadTransform, Transform pointer, Transform ricochetPointer, float offset, Transform randomPointer, float moveForce)
    {
        _moveForce = moveForce;
        _randomPointer = randomPointer;
        _enemyAI = enemyAI;
        _offset = offset;
        _pointer = pointer;
        _ricochetPointer = ricochetPointer;
        _enemyHeadTransform = enemyHeadTransform;
        _rb = rb;
        _pivot = pivot;
        _raySartPoint = raySartPoint;
        _rayDistance = rayDistance;
        _rayColor = rayColor;
        _rayLayer = rayLayer;
        _searcherAnim = searcherAnim;
        _navMeshA = navMeshA;
        _curentNavSpeed = _navMeshA.speed;
        _curentRayDistance = _rayDistance;
        _curentRayDistance2 = _rayDist2;
        _randomRangeX = Random.Range(-30.0f, 30.0f);
        _randomRangeZ = Random.Range(-30.0f, 30.0f);
    }

     private void SearchingEnemy()
     {
        
        Ray ray = new Ray(_raySartPoint.position, _raySartPoint.forward * _rayDistance);
        Debug.DrawRay(ray.origin, ray.direction * _rayDistance, _rayColor);

        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            if(hit.collider.name == "Barricade")
            {
                _rayDistance = Vector3.Distance(DataBase.EnemyData.transform.position, hit.point);
            }else
            {
                _rayDistance = _curentRayDistance;
            }
        }
       
        if(Physics.Raycast(ray,out RaycastHit ht))
        {
            if(ht.collider.name == "Player")
            {
                _isDetected = true;
            }else
            {
                _isDetected = false;
                RicochetSearcher(_isDetected);
            }
        }
     }
    private void SearchingOn()
    {
        if(!_isDetected && !_isRicochetDetect)
        {
            _searcherAnim.enabled = true;
        }else
        {
            _searcherAnim.enabled = false;
        }
    }
     private void PivotLook(bool isRicochetFind)
     {
        _pivot.LookAt(_navMeshA.transform.position);
        if(DataBase.PlayerData != null)
        {
            if (!isRicochetFind)
            {

                _searcherAnim.gameObject.transform.LookAt(DataBase.PlayerData.transform.position);
            }
            else
            {

                _searcherAnim.gameObject.transform.LookAt(_RicochetTargetPosition);
            }
        }
           
       
     }
    private void RicochetSearcher(bool isSearch)
    {
        if(!isSearch)
        {
            Ray ray = new Ray(_raySartPoint.position, _raySartPoint.forward * _rayDistance);
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, _rayColor);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
               

                if (hit.collider.name == "Barricade")
                {
                    _rayDistance = Vector3.Distance(DataBase.EnemyData.transform.position, hit.point);
                  
                   
                    dirr = Vector3.Reflect(ray.direction,hit.normal);
                    _pointer.position = hit.point;
                    
                    Ray RicochetRay = new Ray(_pointer.position, dirr * _rayDist2);
                    Debug.DrawRay(RicochetRay.origin, RicochetRay.direction * _rayDist2, Color.yellow);
                    RaycastHit hit2;
                   
                    if (Physics.Raycast(RicochetRay, out hit2))
                    {
                        dirr2 = Vector3.Reflect(RicochetRay.direction, hit2.normal);
                        _rayDist2 = Vector3.Distance(hit.point, hit2.point);
                        _ricochetPointer.position = hit2.point;
                        //+=+=+=+=
                        if(hit2.collider.name == "Player")
                        {
                            _RicochetTargetPosition = hit.point;
                            _isRicochetDetect = true;
                        }
                        //+=+=+=+=
                        if (hit2.collider.name == "Barricade")
                        {
                            Ray RicochetRay2 = new Ray(_ricochetPointer.position, dirr2 * 1000);
                            Debug.DrawRay(RicochetRay2.origin, RicochetRay2.direction * 1000, _rayColor);
                            if(Physics.Raycast(RicochetRay2,out RaycastHit hit3))
                            {
                                if (hit2.collider.name == "Player")
                                {
                                    _RicochetTargetPosition = hit.point;
                                    _isRicochetDetect = true;
                                }
                            }
                        }
                     
                    }else
                    {
                        _rayDist2 = _curentRayDistance2;
                    }
                }else
                {
                    _rayDistance = _curentRayDistance;
                }
            }
        }

    }
    private void Navigation(bool isDetect)
    {
        float navMoveDist = Vector3.Distance(DataBase.EnemyData.transform.localPosition, _navMeshA.transform.localPosition);
        if(navMoveDist > 5 || navMoveDist < -5)
        {
            _navMeshA.transform.localPosition = DataBase.EnemyData.transform.localPosition;
        }
        if (isDetect)
        {
            _navMeshA.SetDestination(DataBase.PlayerData.transform.position);
        }else
        {
            RandomPointMove();
            
        }
        if(navMoveDist > 0 && navMoveDist <10||navMoveDist >= -5 && navMoveDist <0)
        {
            _navMeshA.speed = _curentNavSpeed;
            
        }
        else
        {
            _navMeshA.speed = 0;

        }
       
    }
    private void RandomPointMove()
    {
        float randomDist = Vector3.Distance(_navMeshA.transform.position, _randomPos);
        _randomPos = new Vector3(_randomRangeX, 1, _randomRangeZ);
        _randomPointer.position = _randomPos;
        if(!_isDetected)
        {
            if (randomDist > 0 && randomDist <= 15 || randomDist < 0 && randomDist >= -15)
            {
                _randomRangeX = Random.Range(-20.0f, 20.0f);
                _randomRangeZ = Random.Range(-130.0f, 110.0f);
            }
            else
            {
              
                _navMeshA.SetDestination(new Vector3(_randomRangeX, 1, _randomRangeZ));
            }
        }
    }
    private void RotationPivot()
    {
        if(DataBase.PlayerData != null)
        {
            if (!_isRicochetDetect)
            {
                Vector3 dir = (DataBase.PlayerData.transform.position - DataBase.EnemyData.transform.position).normalized;
                Quaternion LookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                _enemyHeadTransform.rotation = Quaternion.Lerp(_enemyHeadTransform.rotation, LookRotation, Time.deltaTime * 100.0f);
            }
            if (!_isDetected && _isRicochetDetect)
            {
                Vector3 dir = (_RicochetTargetPosition - DataBase.EnemyData.transform.position).normalized;
                Quaternion LookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                _enemyHeadTransform.rotation = Quaternion.Lerp(_enemyHeadTransform.rotation, LookRotation, Time.deltaTime * 100.0f);
            }
        }
       
        
    }
    private void Movement()
    {
        _rb.AddForce(_rb.transform.forward * _moveForce, ForceMode.Force);
    }
    private void AlgorithmOfSearch()
    {
        if(!_isDetected && _isRicochetDetect)
        {
            tmer += 0.2f * Time.deltaTime;
            if(tmer >= 0.2f)
            {
                tmer = 0.0f;
                _isRicochetDetect = false;
            }
        }
    }
    private void Swipping()
    {
        if(DataBase.IswipData.ISSWIP)
        {

            _swipTimer += 0.2f * Time.deltaTime;
            if(_swipTimer >= 0.2f)
            {
                Debug.Log("swip");
                _swipTimer = 0.0f;
                _rb.AddForce(_rb.transform.right * 300.0f * Random.Range(-1,1) , ForceMode.Impulse);
                DataBase.IswipData.Swip(false);
            }

        }
    }
    private void ShootReson()
    {
        if(_isRicochetDetect)
        {
            _fireTimer += 0.2f * Time.deltaTime;
            if(_fireTimer >= Random.Range(0.1f,0.5f))
            {
                _fireTimer = 0.0f;
              
                    DataBase.EnemyIgunData.Fire(true);
                
                
            }

        }
        if(_isDetected)
        {
            _fireTimer += 0.2f * Time.deltaTime;
            if (_fireTimer >= Random.Range(0.1f, 5.0f))
            {
                _fireTimer = 0.0f;
               
                    DataBase.EnemyIgunData.Fire(true);
               

            }
        }
    }
    public void Tick()
    {
        Navigation(_isDetected);
        PivotLook(_isRicochetDetect);
        RotationPivot();
        SearchingOn();
        AlgorithmOfSearch();
        ShootReson();
        _enemyAI.ShowAngle(_randomRangeX);
        
    }
    public void FixedTick()
    {
        SearchingEnemy();
        Movement();
    }

   }
//============================================================================================================
public class RoundControllerNS
{ 
    private void RoundChangeReason()
    {
        if(!DataBase.EnemyData.activeInHierarchy || !DataBase.PlayerData.activeInHierarchy)
        {
            DataBase.RoundTimerData += 0.2f * Time.deltaTime;
            if(DataBase.RoundTimerData > 2.0f)
            {
                DataBase.RoundTimerData = 0.0f;
                DataBase.PlayerData.transform.position = new Vector3(-17.8f, 2.05f, -129.3f);
                DataBase.PlayerData.SetActive(true);
                GameObject go = GameObject.Find("EnemyP");
                go.transform.position = new Vector3(-1.7f, 2.05f, 64.6f);
                DataBase.EnemyData.transform.localPosition = Vector3.zero;
                GameObject.Find("Navigation").transform.localPosition = Vector3.zero;
                DataBase.EnemyData.SetActive(true);
            }
        }
    }
    public void Tick()
    {
        RoundChangeReason();
    }
}
//============================================================================================================
public class GroundCheckerNS
{
    private Transform _rayStartPoint;
    private GameObject _thisGo;
    private float _rayDist;

    public GroundCheckerNS(Transform rayStartPoint, GameObject thisGo, float rayDist)
    {
        _rayStartPoint = rayStartPoint;
        _thisGo = thisGo;
        _rayDist = rayDist;
    }
    private void RayGroundCheck()
    {
        Ray ray = new Ray(_rayStartPoint.position, -_rayStartPoint.up * _rayDist);
        Debug.DrawRay(ray.origin, ray.direction * _rayDist, Color.blue);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(_thisGo == DataBase.PlayerData)
            {
                if (hit.collider.gameObject.name == "Ground")
                {
                   
                    DataBase.IsPlayerGrounded = true;
                }else
                {
                   
                    DataBase.IsPlayerGrounded = false;
                }
            }
            if (_thisGo == DataBase.EnemyData)
            {
                if (hit.collider.gameObject.name == "Ground")
                {
                    DataBase.IsEnemyGrounded = true;
                }
                else
                {
                    DataBase.IsEnemyGrounded = false;
                }
            }

        }
    }
    public void FixTick()
    {
        RayGroundCheck();
    }
}
//============================================================================================================
  public class SoundPlayerNS
{
    private AudioSource _aSource;
    private AudioClip[] _aclips;
    public SoundPlayerNS(AudioSource aSource, AudioClip[] aclips)
    {
        _aSource = aSource;
        DataBase.AsourceData = aSource;
        _aclips = aclips;
        DataBase.AClipsData = aclips;
       
    }
    private void PlayShootSound()
    {
        if(DataBase.IGunData.ISFIRE)
        {
            _aSource.clip = _aclips[0];
            _aSource.PlayOneShot(_aclips[0]);
        }
        if (DataBase.EnemyIgunData.ISFIRE)
        {
            _aSource.clip = _aclips[0];
            _aSource.PlayOneShot(_aclips[0]);
        }
        if(DataBase.IblowData != null)
        {
            if (DataBase.IblowData.ISBLOW)
            {
                if (_aclips[2] != null)
                {
                    _aSource.clip = _aclips[2];
                    _aSource.PlayOneShot(_aclips[2]);
                    DataBase.IblowData.BlowEnd(false);
                }

            }
        }
      
    }
    public void Tick()
    {
        PlayShootSound();
    }
}
//============================================================================================================
public class DataBase
{
    public static IGun IGunData;
    public static IGun EnemyIgunData;// ≈сли будет много противников сделаю по другому
    public static IplayerController IplayerControllerData;
    public static int PointsData;
    public static int EnemyPointsData;
    public static GameObject DestroyEffectData;
    public static GameObject BlowGoData;
    public static GameObject PlayerData;
    public static GameObject EnemyData;
    public static MeshRenderer[] DestroyEffectMeshRenderData;
    public static Material PlayerMatData;
    public static Material EnemyMatData;
    public static IBlow IblowData;
    public static Iswip IswipData;
    public static float timer;
    public static float RoundTimerData;
    public static bool IsPlayerGrounded;
    public static bool IsEnemyGrounded;
    public static AudioClip[] AClipsData;
    public static AudioSource AsourceData;

    public static  void BlowLifeTimer()
    {
        if (BlowGoData.activeInHierarchy)
        {
            timer += 0.2f * Time.deltaTime;
            if (timer >= 0.1f)
            {
                timer = 0.0f;
                BlowGoData.SetActive(false);
            }
        }
    }

}
//============================================================================================================
