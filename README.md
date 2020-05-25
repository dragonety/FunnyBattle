# 练习项目

https://github.com/dragonety/FunnyBattle

Unity版本2018.4.1

## 简介

此项目的主要目的在于熟悉UNet的通信流程，练习游戏编程模式与常用的架构方法，本次项目学习的过程如下：

1. 学习示例项目的结构
2. 手动创建学习UNet的工程
3. 重构以练习不同的游戏编程模式
4. 在工程上学习并使用AssetBundle
5. 完善当前项目的流程和架构

当前在过程3

## 项目功能

- 管理模块：
  - GameManager记录Player和Magic的NetID
  - EffectManager特效的管理
  - EventManager事件队列的管理，可以传递诸如攻击行为、受伤行为、发射行为
- 战斗玩法：
  - ~~PlayerController包含所有角色控制的内容~~
  - PlayerEntity作为一个player的网络实例，包含一些需要从local调用的方法，并使用“组件模式”将player上的功能拆分
  - 使用“消息队列模式”来传递消息
    - PlayerEntity本身作为NetworkBehaviour来通过SyncVar同步数据
      - 同步的数据包括position rotation health
    - PlayerMoveComponent用来通过input信息控制player移动
      - WASD移动
      - 只有local的player使用
    - PlayerNetMoveComponent控制非localplayer移动
      - 非local的player通过lerp同步的位置信息移动
    - PlayerGraphicComponent控制player动画表现
    
      - 动画信息使用NetworkAnimator同步
      - 左键攻击，右键格挡，E发射魔法球
    - PlayerNetGraphicComponent控制非localplayer动画表现
    
      - player受伤动画
    - PlayerPhysicsComponent主要处理一些逻辑，以及消息的接受
      - 死亡/攻击
    - player发射技能的时候需要向gameManager记录发射物与发射人的dict
  - SwordController控制攻击行为
  - MagicController包含技能的碰撞检测

## 重构

- GameManager管理所有系统
  - 战斗系统
    - BattleModule战斗模块
      - 战斗模块里需要用到UpdateManager/MessageManager/TimerManager/
  
- UpdateManager管理所有update的调用。通过一个static的updateRegister来进行事件的注册

  - UpdateRegister用来注册Update和FixedUpdate事件
    - 最终在component中直接调用RegisterUpdate RegisterFixedUpdate来注册对应的事件

- MessageManager管理所有消息事件。通过一个static的messageRegister来进行事件的注册

  - MessageRegister用来注册调度器Dispatcher

- TimerManager管理所有计时器。通过一个static的timerRegister来进行计时器的注册

  - TimerRegister用来注册计时器TimerTick。还需要额外在UpdateManager里注册TimerManager需要用到的Update事件
    - TimerTick中存放计时器相关的数值

- BattleManager管理战斗相关的manager。需要用到CachedManager/EntityManager

- CachedManager管理缓存相关。通过一个static的EffectCachedPool来管理特效缓存池

  - EffectCachedPool特效缓存池。创建gameObject挂载各类EffectCached
    - EffectCached特效的对象池。使用一个队列来生成、选择特效Effect
      - Effect特效。MonoBehavior，具体实现play/stop/recycle

- EntityManager管理实例。通过一个static的Entities管理具体的实例

  - Entities通过列表字典管理所有的Entity

    - Entity具体的实例。包含components/features/attributes/configs

      - Components使用列表字典管理具体的Component

        - Component组件继承自AbsComponent。包含了Update/FixedUpdate/Message的消息列表，以及对应的注册。

          在component中注册的事件需要同步到entity中

      - Features使用列表字典管理具体的Feature

        - Feature特征继承自AbsFeature。包含unity中的一些实际组件transform/animator等

      - Attributes使用列表字典管理具体的Attribute

        - Attribute属性

      - Configs使用列表字典管理具体的Config

        - Config配置

- 

## 游戏流程

### Host：

进入login场景

点击login进入lobby场景

点击host创建游戏，进入game场景

正常游玩，游戏结束返回lobby

ESC打开UI

点击stopHost关闭并返回lobby场景

### Client:

进入login场景

点击login进入lobby场景

点击client,在有host的情况下可以进入game场景

正常游玩，游戏结束返回lobby

ESC打开UI

点击stopClient关闭并返回lobby场景

## 学习日志

5.19

学习了“组件模式”，应用到了playerController，在应用过程中发现拆分后到component没有合适的消息传播机制，于是学习了“事件队列模式”来处理消息的注册与传递（未完成）

5.20

完成了“事件队列模式”。以去MonoBehaviour的形式进一步重构代码

5.21

重构了除UI外的大部分框架

5.22

除UI外框架梳理结束，开始整理游戏逻辑

5.23

添加了移动组件，分别使用SyncVar和Cmd+Rpc完成

5.24

添加了攻击组件，遇到问题

5.25

优先用老的架构调了一个能用的版本，问题之后再研究