![](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/huaweiicon.png)
## Xamarin.Android.HuaweiMaps
Yet another maps library for Xamarin.Android that optimized for Huawei maps.

Usage is almost the same as  [Xamarin.Forms.Maps](https://www.nuget.org/packages/Xamarin.Forms.Maps), Because this is forked from  [Xamarin.Forms.Maps - github](https://github.com/xamarin/Xamarin.Forms)

## Demo App
You can try DEMO Apps for Android that includes all this library features.  

> Polygon

![Polygon](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/Polygon.jpg)

> Drag Marker
![draglocation](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/draglocation.gif)

> Polylin
![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/Polylin.jpg)

> Circle
![Circle](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/Circle.jpg)

## Motivation
The official  [Xamarin.Forms.Map](https://developer.xamarin.com/guides/xamarin-forms/user-interface/map/)  has minumn functions only.

Especially, Bing Maps SDK is very old-fashioned because it has not vector-tile, marker's infowindow.

Furthermore, I am using Huawei Maps instead of MapKit because it is easy for define common API for Android.


## Comparison with Xamarin.Forms.Maps

 ![enter image description here](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/HuaweiMaps.png)

## Setup
Available on NuGet :(https://www.nuget.org/packages/Xamarin.Android.HuaweiMap/)
-   Please make sure you have  Xamarin.Android.HMSBase as a refernance

## Platform Support
| Android => yes |
|Other =>No|

## Usage
you should add the following code to the your MainActivity.cs after SetContentView 

// MainActivity.cs

         AGConnectServicesConfig config = AGConnectServicesConfig.FromContext(ApplicationContext);
            config.OverlayWith(new HmsLazyInputStream(this));
            Com.Huawei.Agconnect.AGConnectInstance.Initialize(this);

then you need to add your agconnect-services at the Assets folder 
![](https://raw.githubusercontent.com/omernaser/Huawei-MAP/master/Capture.PNG)
please refer to the following link to get it 
[https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0](https://developer.huawei.com/consumer/en/codelab/HMSPreparation/index.html#0)

