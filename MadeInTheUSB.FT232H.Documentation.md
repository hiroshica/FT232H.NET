# MadeInTheUSB FT232H .NET Library

The library MadeInTheUSB.FT232H provides abstraction to program 
* The SPI protocol
* The GPIOS
for the break out based on the FTDI chip ft232H.

Not supported yet:
- I2C 
	* With the FT232H it is not possible to used I2C and SPI at the same time, because the 2 protocol use the same clock and data pin.
	* To support I2C and SPI at the same time you can use the FT2232H or the FT4222H.

- Serial communication
 
## Breakouts available
 
 * The Adafruit breakout: Adafruit FT232H Breakout - General Purpose USB to GPIO+SPI+I2C
	https://www.adafruit.com/product/2264
         
 * The cheap FT232 board from Ebay is supported, though it does not contains an EEPROM
 therefore it is possible to program the device id or description
 https://www.ebay.com/sch/i.html?_from=R40&_trksid=p2380057.m570.l1313.TR11.TRC1.A0.H0.Xft232H.TRS0&_nkw=ft232H&_sacat=0
 There is also now (2018/11) a cheap version for the FT2232H.
 
 * LQFP-48
 http://www.devttys0.com/2011/11/speaking-spi-i2c-with-the-ft-2232/
     from www.devttys0.com: libmpsse https://code.google.com/archive/p/libmpsse/
 

## SPI, I2C, GPIO Wiring

 * SPI
	- Clock AD0
	- MOSI  AD1
	- MISO  AD2
	- CS0   AD3, CS1:AD4, CS2:AD5, CS3:AD6, CS4:AD7

 * I2C
	- Clock   AD0
	- SDA OUT AD1 (need to be connected with SDA IN AD2, Plus pull up )
	- SDA IN  AD2

 * GPIOS
	- GPIO 0..7: AC0..C7. 
	- AC8, AC9 are special and not supported yet by the lirbary
	
 ## .NET Compilation

 * x64 : This code must be compiled in 64 bit mode

 ## Dll dependency and drivers

* The dll FTD2XX.DLL, must be in the path. The dll should be installed by the FTDI driver.
 The driver should automatically be installed by Windows 10 on the first time the FT232H or FT232RL is connected
  to the machine. For Windows 7 install the driver manually.

* This libraty contains the source code of the .NET wrapper for the dll FTD2XX.DLL.
The file is called FTD2XX_NET.cs. This is the last version from FTDT as 2018, that support the FT4222H.

* The dll libMPSSE.dll must be in the current folder.
