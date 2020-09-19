# 逆アセンブル
<div style="text-align: right;">
2020年9月19日<br>
株式会社アルファオメガ  松本清明
</div>

# はじめに

C/C++言語で作ったプログラムはどのような機械語になっているか確認してみよう。

# ソースコードを逆アセンブルしてみる


## Windows コンソールアプリケーションを作る

### VS2019を初めて開き、スタートウィンドウで作成する場合
1. 新しいプロジェクトの作成を選びます。
1. 全ての言語をC++を選びます。
1. コンソールアプリを選び、次へを選びます。
  <img src="./img/スクリーンショット 2020-09-19 072105.png" style="border:1px solid;">
1. プロジェクト名などを決めて作成を選んで下さい。


### すでにVS2019を開いた状態の場合
1. メニューの[ファイル(F)]-[新規作成(N)]-[プロジェクト(P)...]を選択
1. [Visual C++]-[Windowsコンソールアプリケーション]を選択
  ファイル名(N): ConsoleApplication1
  場所(L): 任意
  ソリューションのディレクトリを作成(D) のチェックをつける


## 簡単なC言語のプログラムを逆アセンブラしてみよう
以下のようなソースコード雛形があると思います。
```C++
#include <iostream>

int main()
{
    std::cout << "Hello World!\n";
}
```

## ソースコードにブレークポイントを設定
int main()の横にマウスカーソルを合わせて赤いマークを付ける。又はソースコードにカーソルを合わせ[メニュー]-[デバック]-[ブレークポイントの設定/解除]で設定します。
※F9 キーで設定可能です。


## Debugビルドして 逆アセンブルを確認
1. Debug と x86を選択して 開始又はローカルWindowsデバッカをを起動。
1. ソースコード上のブレークポイントの行でマウス右メニューを開き[逆アセンブルへ移動]を選ぶ。
<img src="./img/スクリーンショット 2020-09-19 062745.png" style="border:1px solid;">
1. 表示オプションのコードバイトの表示を✔をつける
<img src="./img/スクリーンショット 2020-09-19 063221.png" style="border:1px solid;">

以下の様に表示されます。

```
#include <iostream>

int main()
{
00E124E0 55                   push        ebp  
00E124E1 8B EC                mov         ebp,esp  
00E124E3 81 EC C0 00 00 00    sub         esp,0C0h  
00E124E9 53                   push        ebx  
00E124EA 56                   push        esi  
00E124EB 57                   push        edi  
00E124EC 8D BD 40 FF FF FF    lea         edi,[ebp-0C0h]  
00E124F2 B9 30 00 00 00       mov         ecx,30h  
00E124F7 B8 CC CC CC CC       mov         eax,0CCCCCCCCh  
00E124FC F3 AB                rep stos    dword ptr es:[edi]  
00E124FE B9 27 E0 E1 00       mov         ecx,offset _56BECB0D_ConsoleApplication1@cpp (0E1E027h)  
00E12503 E8 64 ED FF FF       call        @__CheckForDebuggerJustMyCode@4 (0E1126Ch)  
    std::cout << "Hello World!\n";
00E12508 68 30 9B E1 00       push        offset string "Hello World!\n" (0E19B30h)  
00E1250D A1 C8 D0 E1 00       mov         eax,dword ptr [__imp_std::cout (0E1D0C8h)]  
00E12512 50                   push        eax  
00E12513 E8 F0 EC FF FF       call        std::operator<<<std::char_traits<char> > (0E11208h)  
00E12518 83 C4 08             add         esp,8  
}
00E1251B 33 C0                xor         eax,eax  
00E1251D 5F                   pop         edi  
00E1251E 5E                   pop         esi  
00E1251F 5B                   pop         ebx  
00E12520 81 C4 C0 00 00 00    add         esp,0C0h  
00E12526 3B EC                cmp         ebp,esp  
00E12528 E8 49 ED FF FF       call        __RTC_CheckEsp (0E11276h)  
00E1252D 8B E5                mov         esp,ebp  
00E1252F 5D                   pop         ebp  
00E12530 C3                   ret  
```

## Releaseビルドして 逆アセンブルを確認
コードが最適化されているのがわかると思います。

```
    std::cout << "Hello World!\n";
00F81000 8B 0D 4C 30 F8 00    mov         ecx,dword ptr [__imp_std::cout (0F8304Ch)]  
00F81006 E8 05 00 00 00       call        std::operator<<<std::char_traits<char> > (0F81010h)  
}
00F8100B 33 C0                xor         eax,eax  
00F8100D C3                   ret  
```


## C言語のプログラムを逆アセンブラしてみよう（その２）
```C++
#include <iostream>

int main()
{
    int number = 0x01234567;
    std::cout << "number=" << number << std::endl;
}
```


## Debugビルドして 逆アセンブルを確認
```
#include <iostream>

int main()
{
008921B0 55                   push        ebp  
008921B1 8B EC                mov         ebp,esp  
008921B3 81 EC CC 00 00 00    sub         esp,0CCh  
008921B9 53                   push        ebx  
008921BA 56                   push        esi  
008921BB 57                   push        edi  
008921BC 8D BD 34 FF FF FF    lea         edi,[ebp-0CCh]  
008921C2 B9 33 00 00 00       mov         ecx,33h  
008921C7 B8 CC CC CC CC       mov         eax,0CCCCCCCCh  
008921CC F3 AB                rep stos    dword ptr es:[edi]  
008921CE B9 27 E0 89 00       mov         ecx,offset _56BECB0D_ConsoleApplication1@cpp (089E027h)  
008921D3 E8 94 F0 FF FF       call        @__CheckForDebuggerJustMyCode@4 (089126Ch)  
    int number = 0x01234567;
008921D8 C7 45 F8 67 45 23 01 mov         dword ptr [number],1234567h  
    std::cout << "number=" << number << std::endl;
008921DF 8B F4                mov         esi,esp  
008921E1 68 06 14 89 00       push        offset std::endl<char,std::char_traits<char> > (0891406h)  
008921E6 8B FC                mov         edi,esp  
008921E8 8B 45 F8             mov         eax,dword ptr [number]  
008921EB 50                   push        eax  
008921EC 68 30 9B 89 00       push        offset string "number=" (0899B30h)  
008921F1 8B 0D C8 D0 89 00    mov         ecx,dword ptr [__imp_std::cout (089D0C8h)]  
008921F7 51                   push        ecx  
008921F8 E8 0B F0 FF FF       call        std::operator<<<std::char_traits<char> > (0891208h)  
008921FD 83 C4 08             add         esp,8  
00892200 8B C8                mov         ecx,eax  
00892202 FF 15 D4 D0 89 00    call        dword ptr [__imp_std::basic_ostream<char,std::char_traits<char> >::operator<< (089D0D4h)]  
00892208 3B FC                cmp         edi,esp  
0089220A E8 67 F0 FF FF       call        __RTC_CheckEsp (0891276h)  
0089220F 8B C8                mov         ecx,eax  
00892211 FF 15 D8 D0 89 00    call        dword ptr [__imp_std::basic_ostream<char,std::char_traits<char> >::operator<< (089D0D8h)]  
00892217 3B F4                cmp         esi,esp  
00892219 E8 58 F0 FF FF       call        __RTC_CheckEsp (0891276h)  
}
0089221E 33 C0                xor         eax,eax  
00892220 5F                   pop         edi  
00892221 5E                   pop         esi  
00892222 5B                   pop         ebx  
00892223 81 C4 CC 00 00 00    add         esp,0CCh  
00892229 3B EC                cmp         ebp,esp  
0089222B E8 46 F0 FF FF       call        __RTC_CheckEsp (0891276h)  
00892230 8B E5                mov         esp,ebp  
00892232 5D                   pop         ebp  
00892233 C3                   ret  
```


## Releaseビルドして 逆アセンブルを確認
```
    int number = 0x01234567;
    std::cout << "number=" << number << std::endl;
003B1000 8B 0D 5C 30 3B 00    mov         ecx,dword ptr [__imp_std::cout (03B305Ch)]  
003B1006 68 10 13 3B 00       push        offset std::endl<char,std::char_traits<char> > (03B1310h)  
003B100B 68 67 45 23 01       push        1234567h  
003B1010 E8 DB 00 00 00       call        std::operator<<<std::char_traits<char> > (03B10F0h)  
003B1015 8B C8                mov         ecx,eax  
003B1017 FF 15 34 30 3B 00    call        dword ptr [__imp_std::basic_ostream<char,std::char_traits<char> >::operator<< (03B3034h)]  
003B101D 8B C8                mov         ecx,eax  
003B101F FF 15 38 30 3B 00    call        dword ptr [__imp_std::basic_ostream<char,std::char_traits<char> >::operator<< (03B3038h)]  
}
003B1025 33 C0                xor         eax,eax  
003B1027 C3                   ret  
```

# アセンブラの内容を理解しよう

## コンパイラ
逆アセンブルしてみてわかるが、CPUが理解できる命令は機械語（アセンブラ）のみであるのだが、人が記述するには大変なので、様々な用途の高級言語は発達しました。
C/C++に代表される高級言語はアセンブラを直接記述することなくコンピューターに命令を記述出来ることである。

## CPUについて
〇Intelのマイクロプロセッサ
現在の多くのパソコンに採用されている。

[8ビット]
4004, 8080

[16ビット]
8086, 80186, 80286

[32ビット]
80386, 80486, Pentium

[64ビット]
Core 2 Duo , Core i7など


〇AMDのマイクロプロセッサ
intelCPU互換で、近年はゲーム機に多い

PS4, Xbox One etc

〇PowerPCのマイクロプロセッサ
WillU, PS3, 

〇ARMのマイクロプロセッサ
 iPhoneシリーズ, Nintendo DS or 3DS



## x86 と x64ってなに？

* x86 , x64 ってなに？

32ビット64ビットを分ける言い方としてx86とx64と表示することがある。

x86のほうが数字が大きいので こちらのほうが性能が良い気がするがx86は32bitを表します。

|型番|bit数|
|:--|:--|
|Intel 8086|16bit|
|Intel 80186|16bit|
|Intel 80286|16bit|
|Intel 80386|32bit|
|Intel Pentium|64bit|
|Intel Core i3,i5,i7|64bit|


## レジスタ
1. Debug と x86を選択して 開始又はローカルWindowsデバッカをを起動。
1. [メニュー]-[デバック]-[ウインドウ]-[レジスタ]でレジスタの内容が確認できます。
  <img src="./img/スクリーンショット 2020-09-19 072927.png" style="border:1px solid;">
1. レジスタウインドウが表示
  <img src="./img/スクリーンショット 2020-09-19 073606.png" style="border:1px solid;">

●汎用レジスタ
  EAX, EBX, ECX, EDX, ESI, EDI, EIP

●スタックポインタタ
  ESP

●スタックベースポインタ
  EBP

●フラグレジスタ
  EFL

## アドレス
32BitのCPUだとすると メモリーは 0x00000000～0xffffffffまである。

1. Debug と x86を選択して 開始又はローカルWindowsデバッカをを起動。
1. [メニュー]-[デバック]-[ウインドウ]-[メモリ]-[メモリ1]でメモリーの表示ができます。
1. レジスタウインドウが表示
  <img src="./img/スクリーンショット 2020-09-19 074726.png" style="border:1px solid;">


# プログラムをさせてプログラムをステップ実行してみよう。

## プログラム１ 配列に書き込む

 調べるプログラムを以下のものとします。
```C++
#include <iostream>

int main()
{
    const size_t size = 4;
    char buf1[size];
    for (int i = 0; i < size; i++)
    {
        buf1[i] = i;
    }
}
```
※ 定数

1. Debug と x86を選択して 開始又はローカルWindowsデバッカをを起動。
1. レジスタウインドウをローカルに切り替える。buf1とiが表示されていると思います。
1. メモリ1ウインドウにbuf1を入力する。0xccが表示されてbuf1のメモリーの内容が見えていると思います。
1. F10キーを押してプログラムをステップ実行する。

1命令ごとにステップ実行してメモリーの内容が変わっていくのを確認してください。書き込みを行った瞬間はメモリーが赤くなったと思います。

逆アセンブル画面でステップ実行して機械語の1命令ずつステップ実行してみて下さい。


## プログラム２ ２つの配列に書き込む
 調べるプログラムを以下のものとします。
```C++
#include <iostream>

int main()
{
    const size_t size = 4;
    char buf1[size];
    char buf2[size];
    for (int i = 0; i < size; i++)
    {
        buf1[i] = i;
    }
    for (int i = 0; i < 16; i++)
    {
        buf2[i] = i;
    }
}
```
このプログラムにはバグがありますわかりますか？

