float4 hueToRGB(float hue)
{
    float r = 0.0;
    float g = 0.0;
    float b = 0.0;
                
    if(hue >= 0.0 && hue < 60.0)
    {
        r = 1.0;
        g = hue/60.0;
        b = 0.0;
    }
    else if(hue >= 60.0 && hue < 120.0)
    {
        r = 1.0 - (hue - 60.0)/60.0;
        g = 1.0;
        b = 0.0;
    }
    else if(hue >= 120.0 && hue < 180.0)
    {
        r = 0.0;
        g = 1.0;
        b = (hue - 120.0)/60.0;
    }
    else if(hue >= 180.0 && hue < 240.0)
    {
        r = 0.0;
        g = 1.0 - (hue - 180.0)/60.0;
        b = 1.0;
    }
    else if(hue >= 240.0 && hue < 300.0)
    {
        r = (hue - 240.0)/60.0;
        g = 0.0;
        b = 1.0;
    }
    else if(hue >= 300.0 && hue <= 360.0)
    {
        r = 1.0;
        g = 0.0;
        b = 1.0 - (hue - 300.0)/60.0;
    }
                
    return float4(r,g,b,1);
}