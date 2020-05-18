# 练习项目

## 简介

此项目的主要目的在于熟悉UNet的通信流程，由于示例工程包含内容很广也很完善，因此希望在学习UNet的同时也学习其他的模块以及架构方法，故而本次项目学习的过程如下：

1. 学习示例项目的结构
2. 手动创建学习UNet的工程
3. 在工程上学习并使用AssetBundle
4. 完善当前项目的流程和架构

当前在过程2

## 项目功能

- 管理模块：
  - GameManager做一些数据上的同步，当前的作用是
  - EffectManager特效的管理
- 资源管理：待完成
- 战斗玩法：
  - 玩家控制player，WASD移动，
  - 玩家左键攻击，右键格挡，E使用技能

## API

|      |      |      |
| ---- | ---- | ---- |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |
|      |      |      |

## 游戏流程

### Host：

进入login场景

点击login进入lobby场景

点击host创建游戏，进入game场景

正常游玩

ESC打开UI

点击stopHost关闭并返回lobby场景

### Client:

进入login场景

点击login进入lobby场景

点击client,在有host的情况下可以进入game场景

正常有望

ESC打开UI

点击stopClient关闭并返回lobby场景

