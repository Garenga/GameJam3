#ifndef D3NOISES
#define D3NOISES





inline float3 random3DVector(float3 position, float offset)
{
    float3x3 m = float3x3(15.27, 47.63, 99.41, 89.98, 74.53, 23.65, 99.53, 56.14, 44.87);
    position = frac(sin(mul(position, m)) * 46839.32);
    return float3(sin(position.y * +offset) * 0.5 + 0.5, cos(position.x * offset) * 0.5 + 0.5, cos(position.z * -+offset + 0.2) * 0.5 + 0.5);
}








void D3Voronoi_float(float3 pos, float angleOffset, float density, out float Out, out float Cells)
{
    float3 fPos = frac(pos * density);
    float3 iPos = floor(pos * density);
    
    float3 result = float3(8, 0, 0);
    
    [Unroll]
    for (int z = -1; z <= 1; z++)
    {
        [Unroll]
        for (int y = -1; y <= 1; y++)
        {
            [Unroll]
            for (int x = -1; x <= 1; x++)
            {
                float3 lattice = float3(x, y, z);
                
                float3 offset = random3DVector(iPos + lattice, angleOffset);
                
                float dist = distance(lattice + offset, fPos);
                
                if (dist < result.x)
                {
                    result = float3(dist, offset.xy);
                    
                    
                    
                }
                
                
                
            }
        }
    }
    
    
    
    
    
    
    
    
    
    Out = result.x;
    Cells = result.y;
}





#endif