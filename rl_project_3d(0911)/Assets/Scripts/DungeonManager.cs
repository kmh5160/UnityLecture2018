using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonManager : MonoBehaviour
{
    public struct Coord
    {
        public int x, y;
        public Coord( int _x, int _y )
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==( Coord c1, Coord c2 )
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=( Coord c1, Coord c2 )
        {
            return !(c1 == c2);
        }
    }

    public Vector2 mapSize = new Vector2( 0, 0 );

    public int seed = -1; // 0보다 작으면 렌덤 시드

    public int[,] roomList;
    List<Coord> roomListCoords;

    Queue<Coord> shuffledCoords;

    [Range( 0.3f, 1 )]
    public float wallPercent = 1; // 벽 비율
    [Range(0f, 1)]
    public float obstaclePercent = 1;
    public int chestNumber;

    public Vector2 curPointDebug;
    Coord curPoint; // 현재 좌표
    Coord exitPoint;

    void Start()
    {
        if( seed < 0 ) seed = Random.Range( 0, int.MaxValue );
        if( curPoint.x < 0 && curPoint.y < 0 )
        {
            curPoint = new Coord( (int)Random.Range( 0, mapSize.x ), (int)Random.Range( 0, mapSize.y ) );
        }
        else
        {
            curPoint.x = (int)curPointDebug.x;
            curPoint.y = (int)curPointDebug.y;
        }
        curPointDebug = new Vector2( curPoint.x, curPoint.y );

        //GenerateMap(); // shuffledCooreds;
        GenerateMap2(); // RandomWalk;
        //GenerateMap3(); //


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMap()
    {
        roomList = null;
        roomList = new int[ (int)mapSize.y, (int)mapSize.x ];
        roomListCoords = new List<Coord>();

        for( int i = 0; i < roomList.Length; i++ ) // 모든 방을 1(길)로 초기화
        {
            roomList.SetValue( 1, i % roomList.GetLength( 0 ), i / roomList.GetLength( 0 ) );
            roomListCoords.Add(
                new Coord( (int)i % roomList.GetLength( 0 )
                         , (int)i / roomList.GetLength( 0 ) ) );
        }

        shuffledCoords = new Queue<Coord>( Utility.ShuffleArray( roomListCoords.ToArray(), seed ) );

        int blankCount = (int)(mapSize.x * mapSize.y * wallPercent);
        int currentBlackCount = 0;

        for( int l = 0; l < blankCount; l++ ) // 빈방 생성
        {
            Coord randomCoord = GetRandomCoord();
            roomList[ randomCoord.x, randomCoord.y ] = 0; // 0: 벽

            currentBlackCount++;
            if( randomCoord != curPoint && MapIsFullyAccessible( roomList, currentBlackCount ) )
            {
                roomList.SetValue( 0, randomCoord.x, randomCoord.y ); // 0 :
            }
            else
            {
                roomList[ randomCoord.x, randomCoord.y ] = 1; // 1: 이동가능
                currentBlackCount--;
            }
        }
        //print( "방번호:"+curPoint.x+","+curPoint.y );

        // 방 모습
        string temp = "";
        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                temp += roomList.GetValue( j, k ) + " ";
            }
            temp += "\n";
        }
        print( temp );


        // 렌덤 생성후 타일 빌드
        if( GetComponentInChildren<Tilemap_Manager>() != null )
        {
            GetComponentInChildren<Tilemap_Manager>().TileBuild();
        }

    }

    public void GenerateMap2()
    {
        roomList = null; // 0:벽 1:길
        roomList = new int[ (int)mapSize.y, (int)mapSize.x ];
        roomListCoords = new List<Coord>();

        System.Random rand = new System.Random(seed);

        int floorX = curPoint.x;
        int floorY = curPoint.y;
        int reqFloorAmount = (int)(mapSize.y * mapSize.x * (1-wallPercent) );
        //print( reqFloorAmount );
        int floorCount = 0;

        // 0 = 벽, 1 = 길
        roomList[ curPoint.y, curPoint.x ] = 1;
        floorCount++;
        while(floorCount < reqFloorAmount)
        {
            int randDir = rand.Next( 4 );

            switch(randDir)
            {
                // up
                case 0:
                {
                    if( (floorY + 1) < roomList.GetLength( 0 )-1 )
                    {
                        floorY++;
                        if(roomList[floorY, floorX] == 0)
                        {
                            //print( "up" );
                            roomList[ floorY, floorX ] = 1;
                            floorCount++;
                            roomListCoords.Add( new Coord( floorX, floorY ) );
                        }
                    } else
                    {
                        floorX = curPoint.x;
                        floorY = curPoint.y;
                    }
                    break;
                }
                // down
                case 1:
                {
                    if( (floorY - 1) > 1 )
                    {
                        floorY--;
                        if( roomList[ floorY, floorX ] == 0 )
                        {

                            //print( "down" );
                            roomList[ floorY, floorX ] = 1;
                            floorCount++;
                            roomListCoords.Add( new Coord( floorX, floorY ) );
                        }
                    } else
                    {
                        floorX = curPoint.x;
                        floorY = curPoint.y;
                    }
                    break;
                }
                // right
                case 2:
                {
                    if( (floorX + 1) < roomList.GetLength( 1 ) - 1 )
                    {
                        floorX++;
                        if( roomList[ floorY, floorX ] == 0 )
                        {
                            //print( "right" );
                            roomList[ floorY, floorX ] = 1;
                            floorCount++;
                            roomListCoords.Add( new Coord( floorX, floorY ) );
                        }
                    } else
                    {
                        floorX = curPoint.x;
                        floorY = curPoint.y;
                    }
                    break;
                }
                // left
                case 3:
                {
                    if( (floorX - 1) > 1 )
                    {
                        floorX--;
                        if( roomList[ floorY, floorX ] == 0 )
                        {
                            //print( "left" );
                            roomList[ floorY, floorX ] = 1;
                            floorCount++;
                            roomListCoords.Add( new Coord( floorX, floorY ) );
                        }
                    } else
                    {
                        floorX = curPoint.x;
                        floorY = curPoint.y;
                    }
                    break;
                }
            }
        }

        CreateEndPoint();
        ObstaclePlacement();
        ChestPlacement();

        RoomViewer();


        // 렌덤 생성후 타일 빌드
        if( GetComponentInChildren<Tilemap_Manager>() != null )
        {
            GetComponentInChildren<Tilemap_Manager>().TileBuild();
        }

    }

    public void GenerateMap3()
    {
        int _xsize;
        int _ysize;

        int totalMapTiles = ((int)mapSize.x - 3) * ((int)mapSize.y - 3) / 2 - 1000;
        int mapTilesCnt = 0;

        roomList = null;
        roomList = new int[ (int)mapSize.y, (int)mapSize.x ];
        roomListCoords = new List<Coord>();

        for( int i = 0; i < roomList.Length; i++ ) // 모든 방을 1(길)로 초기화
        {
            roomList.SetValue( 0, i % roomList.GetLength( 0 ), i / roomList.GetLength( 0 ) );
            roomListCoords.Add(
                new Coord( (int)i % roomList.GetLength( 0 )
                         , (int)i / roomList.GetLength( 0 ) ) );
        }

        shuffledCoords = new Queue<Coord>( Utility.ShuffleArray( roomListCoords.ToArray(), seed ) );

        for( int _y = curPoint.y-1; _y < curPoint.y+1; _y++ )
        {
            for( int _x = curPoint.x-1; _x < curPoint.x+1; _x++ )
            {
                roomList.SetValue( 1, _y, _x );
            }
        }

        while( totalMapTiles > mapTilesCnt)
        {
            refind:
            _xsize = Random.Range( 3, 15 );
            _ysize = Random.Range( 3, 15 );

            Coord randomCoord = GetRandomCoord();

            if( randomCoord.x > mapSize.x - _xsize - 1 || randomCoord.x < 1 )
                continue;
            if( randomCoord.y > mapSize.y - _ysize - 1 || randomCoord.y < 1 )
                continue;

            for( int _y = randomCoord.y-3; _y < randomCoord.y + _ysize +3; _y++ )
            {
                for( int _x = randomCoord.x-3; _x < randomCoord.x + _xsize +3; _x++ )
                {
                    if( _y >= 0 && _x >= 0 && _y < roomList.GetLength(0) && _x < roomList.GetLength(1) )
                    {
                        if( roomList[ _y, _x ] == 1 )
                        {
                            print( mapTilesCnt + "/" + totalMapTiles );
                            goto refind;
                        }
                    }
                }
            }

            for(int _y = randomCoord.y; _y < randomCoord.y+_ysize; _y++ )
            {
                for(int _x = randomCoord.x; _x < randomCoord.x+_xsize; _x++)
                {
                    roomList.SetValue( 1, _y, _x );
                }
            }

            mapTilesCnt += (_xsize+1) * (_ysize+1);
        }

        CreateEndPoint();
        ChestPlacement();

        // 렌덤 생성후 타일 빌드
        if( GetComponentInChildren<Tilemap_Manager>() != null )
        {
            GetComponentInChildren<Tilemap_Manager>().TileBuild();
        }
    }

    // 출구 계산
    private void CreateEndPoint()
    {
        exitPoint = new Coord();
        float rangeMax = 0;
        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                if( roomList[ j, k ] == 1 )
                {
                    if( rangeMax < Mathf.Sqrt( Mathf.Pow( curPoint.y - j, 2 ) + Mathf.Pow( curPoint.x - k, 2 ) ) )
                    {
                        rangeMax = Mathf.Sqrt( Mathf.Pow( curPoint.y - j, 2 ) + Mathf.Pow( curPoint.x - k, 2 ) );

                        exitPoint.x = k;
                        exitPoint.y = j;
                    }
                }
            }
        }
        roomList[ exitPoint.y, exitPoint.x ] = 100;

        print( "(" + exitPoint.y + "," + exitPoint.x + ") :출구와의 거리" + rangeMax );
    }

    private void ObstaclePlacement() // 장애물 생성
    {
        System.Random sysRan = new System.Random( seed );
        //Coord[] obstacleCoord = new Coord[10];

        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                if( roomList[ j, k ]==1 
                    && ( (roomList[j+1, k] == 0 && roomList[j-1, k] == 0 && roomList[j, k+1] == 1 && roomList[j, k-1] == 1) 
                     || roomList[j, k+1] == 0 && roomList[j, k-1] == 0 && roomList[j+1, k] == 1 && roomList[j-1, k] == 1) )
                {
                    int randomIndex = sysRan.Next( 0, 100 );
                    if( randomIndex < obstaclePercent*100 )
                    {
                        roomList[ j, k ] = 10;
                    }
                }
            }
        }
        //roomList[ exitPoint.y, exitPoint.x ] = 10;
    }

    private void ChestPlacement()
    {
        Coord chestPoint = new Coord(); // 상자 좌표
        float rangeMax = 0;
        int chestCnt = 0;
        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                if( roomList[ j, k ] == 1 )
                {
                    if( rangeMax < Mathf.Sqrt( Mathf.Pow( exitPoint.y - j, 2 ) + Mathf.Pow( exitPoint.x - k, 2 ) ) && exitPoint != null )
                    {
                        rangeMax = Mathf.Sqrt( Mathf.Pow( exitPoint.y - j, 2 ) + Mathf.Pow( exitPoint.x - k, 2 ) );

                        chestPoint.x = k;
                        chestPoint.y = j;
                    }
                }
            }
        }
        roomList[ chestPoint.y, chestPoint.x ] = 20;
        chestCnt++;

        shuffledCoords = new Queue<Coord>( Utility.ShuffleArray( roomListCoords.ToArray(), seed ) );

        Coord randomCoord = GetRandomCoord();
        while( chestNumber > chestCnt )
        {
            randomCoord = GetRandomCoord();
            if( roomList[ randomCoord.y, randomCoord.x ] == 1 )
            {
                roomList[ randomCoord.y, randomCoord.x ] = 20;
                chestCnt++;
            }
        }
        print( "ChestPlacement() Complete :: ResultCnt >" + chestCnt );

    }

    private void RoomViewer()
    {
        // 방 모습
        string temp = "";
        for( int j = 0; j < roomList.GetLength( 0 ); j++ )
        {
            for( int k = 0; k < roomList.GetLength( 1 ); k++ )
            {
                temp += roomList.GetValue( j, k ) + " ";

            }
            temp += "\n";
        }
        print( temp );
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledCoords.Dequeue();
        shuffledCoords.Enqueue( randomCoord );
        return randomCoord;
    }

    private bool MapIsFullyAccessible( int[,] map, int currentBlankCnt )
    {
        bool[,] mapFlags = new bool[ map.GetLength( 0 ), map.GetLength( 1 ) ];
        Queue<Coord> queue = new Queue<Coord>();
        mapFlags[ curPoint.y, curPoint.x ] = true;
        queue.Enqueue( curPoint ); // 현재 좌표에서 FloodFill 시작

        int accessibleTileCount = 1;

        while( queue.Count > 0 )
        {
            Coord tile = queue.Dequeue();

            for( int y = -1; y <= 1; y++ )
            {
                for( int x = -1; x <= 1; x++ )
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if( x == 0 || y == 0 )
                    {
                        if( neighbourX >= 0 && neighbourX < map.GetLength( 1 ) && neighbourY >= 0 && neighbourY < map.GetLength( 0 ) )
                        {
                            if( !mapFlags[ neighbourY, neighbourX ] && (map[ neighbourY, neighbourX ] != 0) )
                            {
                                mapFlags[ neighbourY, neighbourX ] = true;
                                queue.Enqueue( new Coord( neighbourX, neighbourY ) );
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }

        }

        int targetAccessibleTilecount = (int)(mapSize.x * mapSize.y - currentBlankCnt);

        return targetAccessibleTilecount == accessibleTileCount;
    }
    
}
