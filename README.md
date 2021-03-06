﻿# KanColleDBPostPlugin

通信データを艦これ統計データベースへ送信する KanColleViewer 用プラグインのようなものを勝手に作りました。

### インストール

* `DBPostPlugin.dll` を KanColleViewer の `Plugins` ディレクトリに移動してください。

### 使い方

* ツール→DBPostタブの艦これ統計データベースへデータを送信しますを有効にし、
アクセスキーを入力することで使用できます。
* 変更した設定は自動的に保存されます。

### ダウンロード

* AppVeyor
  * [![Build status](https://ci.appveyor.com/api/projects/status/fen8euuwhcuv1r5j/branch/master?svg=true)](https://ci.appveyor.com/project/peer4321/kancolledbpostplugin/branch/master/artifacts)
[KanColleDBPostPlugin_Master - AppVeyor](https://ci.appveyor.com/project/peer4321/kancolledbpostplugin/branch/master/artifacts) (安定版)
  * [![Build status](https://ci.appveyor.com/api/projects/status/fen8euuwhcuv1r5j?svg=true)](https://ci.appveyor.com/project/peer4321/kancolledbpostplugin/build/artifacts)
[KanColleDBPostPlugin - AppVeyor](https://ci.appveyor.com/project/peer4321/kancolledbpostplugin/build/artifacts) (最新版)
* GitHub
  * [Releases](https://github.com/peer4321/KanColleDBPostPlugin/releases)
* ダウンロードしたDLLファイルのブロックを忘れずに解除してくださいね。

### チェンジログ

* 1.1.1
  * 建造、開発、戦闘結果の詳細な内容
* 1.1.0
  * 送信ログを表示
  * 送信ログのソート機能
* 1.0.0
  * 送信機能

### ライセンス

* [The MIT License (MIT)](LICENSE)

### 使用ライブラリ

#### [Apache log4net](https://logging.apache.org/log4net/)

* **ライセンス :** Apache License Version 2.0
* **ライセンス全文 :** [licenses/Apache.txt](licenses/Apache.txt)

#### [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)

> The MIT License (MIT)
> 
> Copyright (c) 2013 Grabacr07

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/KanColleViewer.txt](licenses/KanColleViewer.txt)

#### [Livet](http://ugaya40.hateblo.jp/entry/Livet)

* **ライセンス :** zlib/libpng

#### [MetroRadiance](https://github.com/Grabacr07/MetroRadiance)

> The MIT License (MIT)
> 
> Copyright (c) 2014 Manato KAMEYA

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/MetroRadiance.txt](licenses/MetroRadiance.txt)

#### [MetroTrilithon](https://github.com/Grabacr07/MetroTrilithon/blob/master/LICENSE)

> The MIT License (MIT)
> 
> Copyright (c) 2015 Manato KAMEYA

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/MetroTrilithon.txt](licenses/MetroTrilithon.txt)

#### [Nekoxy](https://github.com/veigr/Nekoxy)

> The MIT License (MIT)
> 
> Copyright (c) 2015 veigr

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/Nekoxy.txt](licenses/Nekoxy.txt)

#### [Rx (Reactive Extensions)](https://rx.codeplex.com/)

* **ライセンス :** Apache License Version 2.0
* **ライセンス全文 :** [licenses/Apache.txt](licenses/Apache.txt)

#### [StatefulModel](http://ugaya40.hateblo.jp/entry/StatefulModel)

> The MIT License (MIT)
>
> Copyright (c) 2015 Masanori Onoue

* **用途 :** M-V-Whatever の Model 向けインフラストラクチャ
* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/StatefulModel.txt](licenses/StatefulModel.txt)

### 参考にさせて頂いたもの

このプラグイン作成にあたり、以下のサイトとソースコードを参考にさせて頂きました。

#### [KanColleViewer プラグインの作り方とか - CAT EARS](http://www.cat-ears.net/?p=40454)


#### [about518/KanColleViewer](https://github.com/about518/KanColleViewer/tree/send-database)

> The MIT License (MIT)
> 
> Copyright (c) 2013 Grabacr07

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/KanColleViewer_about518.txt](licenses/KanColleViewer_about518.txt)


#### [Grabacr07/KanColleViewer](https://github.com/Grabacr07/KanColleViewer)

> The MIT License (MIT)
> 
> Copyright (c) 2013 Grabacr07

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/KanColleViewer.txt](licenses/KanColleViewer.txt)

#### [veigr/BattleInfoPlugin](https://github.com/veigr/BattleInfoPlugin)

> The MIT License (MIT)
> 
> Copyright (c) 2015 

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/BattleInfoPlugin.txt](licenses/BattleInfoPlugin.txt)

#### [terry-u16/MaterialChartPlugin](https://github.com/terry-u16/MaterialChartPlugin)

> The MIT License (MIT)
> 
> Copyright (c) 2015 terry_u16

* **ライセンス :** The MIT License (MIT)
* **ライセンス全文 :** [licenses/MaterialChartPlugin.txt](licenses/MaterialChartPlugin.txt)
