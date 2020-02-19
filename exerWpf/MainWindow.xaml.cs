using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace exerWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int level = 0;
        private int countBoxs = 0;
        private int countEnemy = 0;
        private int enemyInLvl = 0;
        private int boxIsGet = 0;
        private List<ContentControl> stackBox;
        private List<ContentControl> stackEnemy;
        private bool humanCapture;
        private Random random;
        private Robotfactory fact;
        private Robot robot;
        private History GameHistory;
        private Proxy proxy;
        private Earth earth;
        private RobotCommand command;
        private DispatcherTimer enemyTimer;
        private Storyboard storyboardHuman;
        private DispatcherTimer winTimer;
        public MainWindow()
        {
            InitializeComponent();
            HelpWin helpWin = new HelpWin();
            helpWin.ShowDialog();
            helpWin.Close();
            humanCapture = false;
            InitSysRobot();
            GetTemplRobot();
            InitStatus();
            enemyTimer = new DispatcherTimer();
            enemyTimer.Tick += EnemyTimer_Tick;
            enemyTimer.Interval = TimeSpan.FromSeconds(5);
            winTimer = new DispatcherTimer();
            winTimer.Tick += LevelUp;
            winTimer.Interval = TimeSpan.FromSeconds(2);
            levelInfo();

        }

        private void LevelUp(object sender, EventArgs e)
        {
                lvlShow.Visibility = Visibility.Hidden;
                lvllebl.Visibility = Visibility.Hidden;
        }

        private void EnemyTimer_Tick(object sender, EventArgs e)
        {
            AddEnemy();
        }

        private void levelInfo()
        {
            enemyInLvl += level * countEnemy + 1;
        }
        private void InitSysRobot()
        {
            random = new Random();
            stackBox = new List<ContentControl>();
            stackEnemy = new List<ContentControl>();
            fact = new Robotfactory();
            robot = fact.GetRobot(random);
            GameHistory = new History();
            double xHuman = Canvas.GetLeft(human);
            double yHuman = Canvas.GetTop(human);
            GameHistory.Save(robot.State(xHuman, yHuman));
            earth = new Earth();
            command = new RobotCommand(robot);
            storyboardHuman  = new Storyboard() { AutoReverse = false };
            level = 1;
            countEnemy = 1;
            boxIsGet = 7;
        }

        private void InitStatus()
        {
            status.Maximum = robot.chargeOfRobot;
            status.Value = robot.chargeOfRobot;
            statusMass.Maximum = robot.MaxMassCargo;
            statusMass.Value = robot.sumOfCargo;
        }
        private void GetTemplRobot()
        {
            string type = robot.GetType().Name;
            if(type == "KiborgRobot")
            {
                myRobot.Template = Resources["r2d2"] as ControlTemplate;
                avatar.Template = Resources["avaR2d2"] as ControlTemplate;
            }
            else if(type == "SaiceRobot")
            {
                myRobot.Template = Resources["s3po"] as ControlTemplate;
                avatar.Template = Resources["avaS3po"] as ControlTemplate;
            }
            else
            {
                myRobot.Template = Resources["bb8"] as ControlTemplate;
                avatar.Template = Resources["avaBb8"] as ControlTemplate;
            }
        }

        private double GetPosHumanX()
        {
            return Canvas.GetLeft(human);
        }

        private double GetPosHumanY()
        {
            return Canvas.GetTop(human);
        }
        private void AddBox()
        {
            ContentControl box;
            int isBox = random.Next(1, 1000);
            if (isBox > 30)
            {
                box = new ContentControl
                {
                    Template =  Resources["boxForRob"] as ControlTemplate
                };
                box.Name = "box";
            }
            else
            {
                box = new ContentControl
                {
                    Template = Resources["helthBox"] as ControlTemplate
                };
                box.Name = "helth";
            }
            Canvas.SetLeft(box, random.Next(100, (int)playArea.ActualWidth - 100));
            Canvas.SetTop(box, random.Next(100, (int) playArea.ActualHeight - 100));
            playArea.Children.Add(box);
            stackBox.Add(box);
            box.MouseEnter += BoxMouseEntered;

        }
        private void AddEnemy()
        {
            if (enemyInLvl > stackEnemy.Count)
            {
                int randEnemy = random.Next(1, 1000);
                ContentControl enemy;
                if (randEnemy < 300)
                {
                    enemy = new ContentControl
                    {
                        Template = Resources["enemyVayd"] as ControlTemplate
                    };
                }
                else if (randEnemy > 300 && randEnemy < 600)
                {
                    enemy = new ContentControl
                    {
                        Template = Resources["enemyBen"] as ControlTemplate
                    };
                }
                else
                {
                    enemy = new ContentControl
                    {
                        Template = Resources["shooter"] as ControlTemplate
                    };
                }
                AnimateEnemy(enemy, 0, playArea.ActualWidth - 70, "(Canvas.Left)");
                AnimateEnemy(enemy, random.Next((int) playArea.ActualHeight - 70),
                    random.Next((int) playArea.ActualHeight - 70), "(Canvas.Top)");
                playArea.Children.Add(enemy);
                stackEnemy.Add(enemy);
                enemy.MouseEnter += Enemy_MouseEnter;
            }
        }

        private void Enemy_MouseEnter(object sender, MouseEventArgs e)
        {
            if (humanCapture)
            {
                robot.chargeOfRobot -= 100;
                StatusUpdate();
            }
        }

        private void AnimateEnemy(ContentControl enemy, double from, double to, string property)
        {
            float speed = 6 - (level / 2);
            Storyboard storyboard = new Storyboard() { AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(speed))
            };
            Storyboard.SetTarget(animation, enemy);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            storyboard.Children.Add(animation);
            storyboard.Begin();

        }

        private void BoxMouseEntered(object sender, MouseEventArgs e)
        {
            if (humanCapture)
            {
                double xHuman = GetPosHumanX();
                double yHuman = GetPosHumanY();
                int indexEnemy = -1;
                double minDistance = 10000;
                for (int i = 0; i < stackBox.Count; i++)
                {
                    double xEnemy = Canvas.GetLeft(stackBox[i]);
                    double yEnemy = Canvas.GetTop(stackBox[i]);
                    double Distance = Math.Sqrt(Math.Pow((xHuman - xEnemy), 2) + Math.Pow((yHuman - yEnemy), 2));
                    if (Distance < minDistance)
                    {
                        minDistance = Distance;
                        indexEnemy = i;
                    }

                }
                if (stackBox[indexEnemy].Name == "helth")
                {
                    robot.chargeOfRobot += 100;
                }
                else
                {
                    proxy = new Proxy(robot)
                    {
                        box = earth.GetBox()
                    };
                    //MessageBox.Show(String.Format("Price: {0} \rMass: {1} \rХотите подобрать ?",proxy.box.Price, proxy.box.Mass ), "My App", MessageBoxButton.YesNoCancel);
                    GameHistory.Save(robot.State(xHuman, yHuman));
                    robot.GetGrooz(proxy);
                    radAct.IsChecked = (proxy.box.Toxic == true) ? IsEnabled : IsSealed;
                    countBoxs++;
                    IsWin();
                }
                DelBoxFromPlayArea(indexEnemy);
                AddBox();
                StatusUpdate();
            }
        }

        private void IsWin()
        {
            if (countBoxs == boxIsGet)
            {
                countBoxs = 0;
                winTimer.Start();
                ClearStackBoxOrEnemy(stackEnemy);
                level++;
                boxIsGet++;
                levelInfo();
                lvlShow.Text = level.ToString();
                lvlShow.Visibility = Visibility.Visible;
                lvllebl.Visibility = Visibility.Visible;

            }
        }
        private void StatusUpdate()
        {
            status.Value = robot.chargeOfRobot;
            statusMass.Value = robot.sumOfCargo;
            countBox.Text = countBoxs.ToString();
        }

        private void DelBoxFromPlayArea(int indexForDel)
        {
            playArea.Children.Remove(stackBox[indexForDel]);
            stackBox.RemoveAt(indexForDel);
        }
        private void PlayArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (humanCapture)
            {
                Point pointerPosition = e.GetPosition(null);
                Point relativePosition = playArea.TransformToVisual(playArea).Transform(pointerPosition);
                if ((Math.Abs(relativePosition.X - Canvas.GetLeft(human)) > human.ActualWidth * 3) ||
                    (Math.Abs(relativePosition.Y - Canvas.GetTop(human)) > human.ActualHeight * 3))
                {
                    humanCapture = false;
                    human.IsHitTestVisible = true;

                }
                else
                {
                    robot.chargeOfRobot -= 0.35;
                    status.Value = robot.chargeOfRobot;
                    Canvas.SetLeft(human, relativePosition.X - human.ActualWidth / 2);
                    Canvas.SetTop(human, relativePosition.Y - human.ActualHeight / 0.8);
                }
            }
        }
        private void Titul_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Human_MouseDown(object sender, MouseButtonEventArgs e)
        {
            humanCapture = true;
            human.IsHitTestVisible = false;
            this.Cursor = Cursors.None;

        }
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            command.Undo(GameHistory);
            status.Value = robot.chargeOfRobot;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                AddBox();
            }
            if (e.Key == Key.A)
            {
                humanCapture = false;
                human.IsHitTestVisible = true;
            }
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((GetPosHumanX() == 10) && (GetPosHumanY() == 10) && (storyboardHuman.Children.Count == 0))
            {
                enemyTimer.Start();
                AddBox();
                AddEnemy();
                lvlShow.Visibility = Visibility.Hidden;
                lvllebl.Visibility = Visibility.Hidden;
            }
            else if ((GetPosHumanX() == 10) && (GetPosHumanY() == 10))
            {
                storyboardHuman.Stop();
                storyboardHuman.Children.RemoveAt(0);
                Canvas.SetLeft(human, 10);
                Canvas.SetTop(human, 10);
                enemyTimer.Start();
                level = 1;
                boxIsGet = 7;
                countBoxs = 0;
                levelInfo();
                gameOver.Visibility = Visibility.Hidden;
                AddBox();
                AddEnemy();

            }
        }

        private void GameIsOver()
        {
            gameOver.Visibility = Visibility;
            HumanMoveToStart(GetPosHumanX(),10,"(Canvas.Left)");
            HumanMoveToStart(GetPosHumanY(),10,"(Canvas.Top)");
            ClearStackBoxOrEnemy(stackBox);
            ClearStackBoxOrEnemy(stackEnemy);
            enemyTimer.Stop();
            startBut.IsChecked = IsSealed;
            this.Cursor = Cursors.Arrow;

        }

        private void HumanMoveToStart(double from, double to, string property)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            Storyboard.SetTarget(animation, human);
            Storyboard.SetTargetProperty(animation, new PropertyPath(property));
            storyboardHuman.Children.Add(animation);
            storyboardHuman.Begin();
        }

        private void ClearStackBoxOrEnemy(List<ContentControl> steckForClear)
        {
            if (steckForClear.Count != 0)
            {
                for (int i = 0; i < steckForClear.Count; i++)
                {
                    playArea.Children.Remove(steckForClear[i]);
                }
                steckForClear.Clear();
            }
            
        }

        private void playArea_MouseLeave(object sender, MouseEventArgs e)
        {
            if (humanCapture)
            {
                GameIsOver();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
