using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Manager : MonoBehaviour {
    public Tilemap ceiling_tilemap;
    public Tilemap floor_tilemap;
    public Tilemap wall_tilemap;
    public Tilemap wall_tilemap_r;
    public Tilemap wall_tilemap_l;
    public Transform Colliders;

    public RuleTile tile;
    public Tile wall_tile;
    public GameObject colliderBox;
    public GameObject exit;
    public GameObject obstacle;
    public GameObject chest;

    private int[,] roomList;

	void Start () {
        //ceiling_tilemap.SetTile( new Vector3Int( 9, 1, 0 ), tile );
        //TileBuild();
	}

    void Update () {
		
	}

    public void TileBuild()
    {
        this.roomList = GetComponentInParent<DungeonManager>().roomList;

        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                if(roomList[ j, k ] == 0) // 0 : 벽 타일
                {
                    ceiling_tilemap.SetTile( new Vector3Int( k, j, 0 ), tile );
                    wall_tilemap.SetTile( new Vector3Int( k, j, 0 ), wall_tile );
                    wall_tilemap_r.SetTile( new Vector3Int( k, j, 0 ), wall_tile );
                    wall_tilemap_l.SetTile( new Vector3Int( k, j, 0 ), wall_tile );
                    GameObject.Instantiate( colliderBox, new Vector3( (k + 0.5f)*GetComponentInParent<Transform>().lossyScale.x, 0, (j + 0.5f)*GetComponentInParent<Transform>().lossyScale.y ), Quaternion.AngleAxis(90f, Vector3.right), Colliders );
                } else { // 1 : 이동가능 타일
                    floor_tilemap.SetTile( new Vector3Int( k, j, 0 ), tile );
                }

                if( roomList[ j, k ] == 100 ) // 100 : 출구 
                {
                    Instantiate( exit, new Vector3( (k + 0.5f) * GetComponentInParent<Transform>().lossyScale.x, 0, (j + 0.5f) * GetComponentInParent<Transform>().lossyScale.y ), Quaternion.AngleAxis( 90f, Vector3.right ), transform );
                }

                if( roomList[ j, k ] == 10 ) // 10 : 장애물
                {
                    Instantiate( obstacle, new Vector3( (k + 0.5f) * GetComponentInParent<Transform>().lossyScale.x, 0, (j + 0.5f) * GetComponentInParent<Transform>().lossyScale.y ), Quaternion.AngleAxis( 0f, Vector3.right ), transform );
                }

                if( roomList[ j, k ] == 20 ) // 20 : 상자
                {
                    Instantiate( chest, new Vector3( (k + 0.5f) * GetComponentInParent<Transform>().lossyScale.x, 0, (j + 0.5f) * GetComponentInParent<Transform>().lossyScale.y ), Quaternion.AngleAxis( 0f, Vector3.right ), transform );
                }

            }
        }
    }

}
