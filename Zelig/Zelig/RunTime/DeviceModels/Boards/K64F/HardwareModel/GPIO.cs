﻿//
// Copyright ((c) Microsoft Corporation.    All rights reserved.
//

namespace Microsoft.Zelig.K64F
{

    //--//

    public enum PinDirection
    {
        PIN_INPUT,
        PIN_OUTPUT
    }

    public enum PinMode
    {
        PullUp      = 0,
        PullDown    = 3,
        PullNone    = 2,
        Repeater    = 1,
        OpenDrain   = 4,
        PullDefault = PullDown
    }
    
    public enum PinName : uint
    {
        // Commented out pins do not appear to be used on the board
        PTA0  = ((0 << Board.GPIO_PORT_SHIFT) | 0 ),
        PTA1  = ((0 << Board.GPIO_PORT_SHIFT) | 1 ),
        PTA2  = ((0 << Board.GPIO_PORT_SHIFT) | 2 ),
        PTA3  = ((0 << Board.GPIO_PORT_SHIFT) | 3 ),
        PTA4  = ((0 << Board.GPIO_PORT_SHIFT) | 4 ),
        PTA5  = ((0 << Board.GPIO_PORT_SHIFT) | 5 ),
        PTA6  = ((0 << Board.GPIO_PORT_SHIFT) | 6 ),
        PTA7  = ((0 << Board.GPIO_PORT_SHIFT) | 7 ),
        PTA8  = ((0 << Board.GPIO_PORT_SHIFT) | 8 ),
        PTA9  = ((0 << Board.GPIO_PORT_SHIFT) | 9 ),
        PTA10 = ((0 << Board.GPIO_PORT_SHIFT) | 10),
        PTA11 = ((0 << Board.GPIO_PORT_SHIFT) | 11),
        PTA12 = ((0 << Board.GPIO_PORT_SHIFT) | 12),
        PTA13 = ((0 << Board.GPIO_PORT_SHIFT) | 13),
        PTA14 = ((0 << Board.GPIO_PORT_SHIFT) | 14),
        PTA15 = ((0 << Board.GPIO_PORT_SHIFT) | 15),
        PTA16 = ((0 << Board.GPIO_PORT_SHIFT) | 16),
        PTA17 = ((0 << Board.GPIO_PORT_SHIFT) | 17),
        PTA18 = ((0 << Board.GPIO_PORT_SHIFT) | 18),
        PTA19 = ((0 << Board.GPIO_PORT_SHIFT) | 19),
        PTA20 = ((0 << Board.GPIO_PORT_SHIFT) | 20),
        PTA21 = ((0 << Board.GPIO_PORT_SHIFT) | 21),
        PTA22 = ((0 << Board.GPIO_PORT_SHIFT) | 22),
        PTA23 = ((0 << Board.GPIO_PORT_SHIFT) | 23),
        PTA24 = ((0 << Board.GPIO_PORT_SHIFT) | 24),
        PTA25 = ((0 << Board.GPIO_PORT_SHIFT) | 25),
        PTA26 = ((0 << Board.GPIO_PORT_SHIFT) | 26),
        PTA27 = ((0 << Board.GPIO_PORT_SHIFT) | 27),
        PTA28 = ((0 << Board.GPIO_PORT_SHIFT) | 28),
        PTA29 = ((0 << Board.GPIO_PORT_SHIFT) | 29),
        PTA30 = ((0 << Board.GPIO_PORT_SHIFT) | 30),
        PTA31 = ((0 << Board.GPIO_PORT_SHIFT) | 31),
        PTB0  = ((1 << Board.GPIO_PORT_SHIFT) | 0 ),
        PTB1  = ((1 << Board.GPIO_PORT_SHIFT) | 1 ),
        PTB2  = ((1 << Board.GPIO_PORT_SHIFT) | 2 ),
        PTB3  = ((1 << Board.GPIO_PORT_SHIFT) | 3 ),
        PTB4  = ((1 << Board.GPIO_PORT_SHIFT) | 4 ),
        PTB5  = ((1 << Board.GPIO_PORT_SHIFT) | 5 ),
        PTB6  = ((1 << Board.GPIO_PORT_SHIFT) | 6 ),
        PTB7  = ((1 << Board.GPIO_PORT_SHIFT) | 7 ),
        PTB8  = ((1 << Board.GPIO_PORT_SHIFT) | 8 ),
        PTB9  = ((1 << Board.GPIO_PORT_SHIFT) | 9 ),
        PTB10 = ((1 << Board.GPIO_PORT_SHIFT) | 10),
        PTB11 = ((1 << Board.GPIO_PORT_SHIFT) | 11),
        PTB12 = ((1 << Board.GPIO_PORT_SHIFT) | 12),
        PTB13 = ((1 << Board.GPIO_PORT_SHIFT) | 13),
        PTB14 = ((1 << Board.GPIO_PORT_SHIFT) | 14),
        PTB15 = ((1 << Board.GPIO_PORT_SHIFT) | 15),
        PTB16 = ((1 << Board.GPIO_PORT_SHIFT) | 16),
        PTB17 = ((1 << Board.GPIO_PORT_SHIFT) | 17),
        PTB18 = ((1 << Board.GPIO_PORT_SHIFT) | 18),
        PTB19 = ((1 << Board.GPIO_PORT_SHIFT) | 19),
        PTB20 = ((1 << Board.GPIO_PORT_SHIFT) | 20),
        PTB21 = ((1 << Board.GPIO_PORT_SHIFT) | 21),
        PTB22 = ((1 << Board.GPIO_PORT_SHIFT) | 22),
        PTB23 = ((1 << Board.GPIO_PORT_SHIFT) | 23),
        PTB24 = ((1 << Board.GPIO_PORT_SHIFT) | 24),
        PTB25 = ((1 << Board.GPIO_PORT_SHIFT) | 25),
        PTB26 = ((1 << Board.GPIO_PORT_SHIFT) | 26),
        PTB27 = ((1 << Board.GPIO_PORT_SHIFT) | 27),
        PTB28 = ((1 << Board.GPIO_PORT_SHIFT) | 28),
        PTB29 = ((1 << Board.GPIO_PORT_SHIFT) | 29),
        PTB30 = ((1 << Board.GPIO_PORT_SHIFT) | 30),
        PTB31 = ((1 << Board.GPIO_PORT_SHIFT) | 31),
        PTC0  = ((2 << Board.GPIO_PORT_SHIFT) | 0 ),
        PTC1  = ((2 << Board.GPIO_PORT_SHIFT) | 1 ),
        PTC2  = ((2 << Board.GPIO_PORT_SHIFT) | 2 ),
        PTC3  = ((2 << Board.GPIO_PORT_SHIFT) | 3 ),
        PTC4  = ((2 << Board.GPIO_PORT_SHIFT) | 4 ),
        PTC5  = ((2 << Board.GPIO_PORT_SHIFT) | 5 ),
        PTC6  = ((2 << Board.GPIO_PORT_SHIFT) | 6 ),
        PTC7  = ((2 << Board.GPIO_PORT_SHIFT) | 7 ),
        PTC8  = ((2 << Board.GPIO_PORT_SHIFT) | 8 ),
        PTC9  = ((2 << Board.GPIO_PORT_SHIFT) | 9 ),
        PTC10 = ((2 << Board.GPIO_PORT_SHIFT) | 10),
        PTC11 = ((2 << Board.GPIO_PORT_SHIFT) | 11),
        PTC12 = ((2 << Board.GPIO_PORT_SHIFT) | 12),
        PTC13 = ((2 << Board.GPIO_PORT_SHIFT) | 13),
        PTC14 = ((2 << Board.GPIO_PORT_SHIFT) | 14),
        PTC15 = ((2 << Board.GPIO_PORT_SHIFT) | 15),
        PTC16 = ((2 << Board.GPIO_PORT_SHIFT) | 16),
        PTC17 = ((2 << Board.GPIO_PORT_SHIFT) | 17),
        PTC18 = ((2 << Board.GPIO_PORT_SHIFT) | 18),
        PTC19 = ((2 << Board.GPIO_PORT_SHIFT) | 19),
        PTC20 = ((2 << Board.GPIO_PORT_SHIFT) | 20),
        PTC21 = ((2 << Board.GPIO_PORT_SHIFT) | 21),
        PTC22 = ((2 << Board.GPIO_PORT_SHIFT) | 22),
        PTC23 = ((2 << Board.GPIO_PORT_SHIFT) | 23),
        PTC24 = ((2 << Board.GPIO_PORT_SHIFT) | 24),
        PTC25 = ((2 << Board.GPIO_PORT_SHIFT) | 25),
        PTC26 = ((2 << Board.GPIO_PORT_SHIFT) | 26),
        PTC27 = ((2 << Board.GPIO_PORT_SHIFT) | 27),
        PTC28 = ((2 << Board.GPIO_PORT_SHIFT) | 28),
        PTC29 = ((2 << Board.GPIO_PORT_SHIFT) | 29),
        PTC30 = ((2 << Board.GPIO_PORT_SHIFT) | 30),
        PTC31 = ((2 << Board.GPIO_PORT_SHIFT) | 31),
        PTD0  = ((3 << Board.GPIO_PORT_SHIFT) | 0 ),
        PTD1  = ((3 << Board.GPIO_PORT_SHIFT) | 1 ),
        PTD2  = ((3 << Board.GPIO_PORT_SHIFT) | 2 ),
        PTD3  = ((3 << Board.GPIO_PORT_SHIFT) | 3 ),
        PTD4  = ((3 << Board.GPIO_PORT_SHIFT) | 4 ),
        PTD5  = ((3 << Board.GPIO_PORT_SHIFT) | 5 ),
        PTD6  = ((3 << Board.GPIO_PORT_SHIFT) | 6 ),
        PTD7  = ((3 << Board.GPIO_PORT_SHIFT) | 7 ),
        PTD8  = ((3 << Board.GPIO_PORT_SHIFT) | 8 ),
        PTD9  = ((3 << Board.GPIO_PORT_SHIFT) | 9 ),
        PTD10 = ((3 << Board.GPIO_PORT_SHIFT) | 10),
        PTD11 = ((3 << Board.GPIO_PORT_SHIFT) | 11),
        PTD12 = ((3 << Board.GPIO_PORT_SHIFT) | 12),
        PTD13 = ((3 << Board.GPIO_PORT_SHIFT) | 13),
        PTD14 = ((3 << Board.GPIO_PORT_SHIFT) | 14),
        PTD15 = ((3 << Board.GPIO_PORT_SHIFT) | 15),
        PTD16 = ((3 << Board.GPIO_PORT_SHIFT) | 16),
        PTD17 = ((3 << Board.GPIO_PORT_SHIFT) | 17),
        PTD18 = ((3 << Board.GPIO_PORT_SHIFT) | 18),
        PTD19 = ((3 << Board.GPIO_PORT_SHIFT) | 19),
        PTD20 = ((3 << Board.GPIO_PORT_SHIFT) | 20),
        PTD21 = ((3 << Board.GPIO_PORT_SHIFT) | 21),
        PTD22 = ((3 << Board.GPIO_PORT_SHIFT) | 22),
        PTD23 = ((3 << Board.GPIO_PORT_SHIFT) | 23),
        PTD24 = ((3 << Board.GPIO_PORT_SHIFT) | 24),
        PTD25 = ((3 << Board.GPIO_PORT_SHIFT) | 25),
        PTD26 = ((3 << Board.GPIO_PORT_SHIFT) | 26),
        PTD27 = ((3 << Board.GPIO_PORT_SHIFT) | 27),
        PTD28 = ((3 << Board.GPIO_PORT_SHIFT) | 28),
        PTD29 = ((3 << Board.GPIO_PORT_SHIFT) | 29),
        PTD30 = ((3 << Board.GPIO_PORT_SHIFT) | 30),
        PTD31 = ((3 << Board.GPIO_PORT_SHIFT) | 31),
        PTE0  = ((4 << Board.GPIO_PORT_SHIFT) | 0 ),
        PTE1  = ((4 << Board.GPIO_PORT_SHIFT) | 1 ),
        PTE2  = ((4 << Board.GPIO_PORT_SHIFT) | 2 ),
        PTE3  = ((4 << Board.GPIO_PORT_SHIFT) | 3 ),
        PTE4  = ((4 << Board.GPIO_PORT_SHIFT) | 4 ),
        PTE5  = ((4 << Board.GPIO_PORT_SHIFT) | 5 ),
        PTE6  = ((4 << Board.GPIO_PORT_SHIFT) | 6 ),
        PTE7  = ((4 << Board.GPIO_PORT_SHIFT) | 7 ),
        PTE8  = ((4 << Board.GPIO_PORT_SHIFT) | 8 ),
        PTE9  = ((4 << Board.GPIO_PORT_SHIFT) | 9 ),
        PTE10 = ((4 << Board.GPIO_PORT_SHIFT) | 10),
        PTE11 = ((4 << Board.GPIO_PORT_SHIFT) | 11),
        PTE12 = ((4 << Board.GPIO_PORT_SHIFT) | 12),
        PTE13 = ((4 << Board.GPIO_PORT_SHIFT) | 13),
        PTE14 = ((4 << Board.GPIO_PORT_SHIFT) | 14),
        PTE15 = ((4 << Board.GPIO_PORT_SHIFT) | 15),
        PTE16 = ((4 << Board.GPIO_PORT_SHIFT) | 16),
        PTE17 = ((4 << Board.GPIO_PORT_SHIFT) | 17),
        PTE18 = ((4 << Board.GPIO_PORT_SHIFT) | 18),
        PTE19 = ((4 << Board.GPIO_PORT_SHIFT) | 19),
        PTE20 = ((4 << Board.GPIO_PORT_SHIFT) | 20),
        PTE21 = ((4 << Board.GPIO_PORT_SHIFT) | 21),
        PTE22 = ((4 << Board.GPIO_PORT_SHIFT) | 22),
        PTE23 = ((4 << Board.GPIO_PORT_SHIFT) | 23),
        PTE24 = ((4 << Board.GPIO_PORT_SHIFT) | 24),
        PTE25 = ((4 << Board.GPIO_PORT_SHIFT) | 25),
        PTE26 = ((4 << Board.GPIO_PORT_SHIFT) | 26),
        PTE27 = ((4 << Board.GPIO_PORT_SHIFT) | 27),
        PTE28 = ((4 << Board.GPIO_PORT_SHIFT) | 28),
        PTE29 = ((4 << Board.GPIO_PORT_SHIFT) | 29),
        PTE30 = ((4 << Board.GPIO_PORT_SHIFT) | 30),
        PTE31 = ((4 << Board.GPIO_PORT_SHIFT) | 31),

        LED_RED   = PTB22,
        LED_GREEN = PTE26,
        LED_BLUE  = PTB21,

        // mbed original LED naming
        LED1 = LED_RED,
        LED2 = LED_GREEN,
        LED3 = LED_BLUE,
        LED4 = LED_RED,

        //Push buttons
        SW2 = PTC6,
        SW3 = PTA4,

        // USB Pins
        USBTX = PTB17,
        USBRX = PTB16,

        // Arduino Headers
        D0 = PTC16,
        D1 = PTC17,
        D2 = PTB9,
        D3 = PTA1,
        D4 = PTB23,
        D5 = PTA2,
        D6 = PTC2,
        D7 = PTC3,
        D8 = PTA0,
        D9 = PTC4,
        D10 = PTD0,
        D11 = PTD2,
        D12 = PTD3,
        D13 = PTD1,
        D14 = PTE25,
        D15 = PTE24,
    
        I2C_SCL = D15,
        I2C_SDA = D14,

        A0 = PTB2,
        A1 = PTB3,
        A2 = PTB10,
        A3 = PTB11,
        A4 = PTC11,
        A5 = PTC10,

        DAC0_OUT = 0xFEFE, /* DAC does not have Pin Name in RM */

        // Not connected
        NC = 0xFFFFFFFF
    }    
}
