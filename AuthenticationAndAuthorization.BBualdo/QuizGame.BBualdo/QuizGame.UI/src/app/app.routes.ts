import { Routes } from '@angular/router';
import { MainMenuComponent } from '../components/main-menu/main-menu.component';
import { QuizManagerLayout } from '../components/Quiz-Management/quiz-manager-layout/quiz-manager.component';
import { QuizDetailsComponent } from '../components/Quiz-Management/quiz-details/quiz-details.component';
import { QuizListComponent } from '../components/Quiz-Management/quiz-list/quiz-list.component';
import { CreateQuizComponent } from '../components/Quiz-Management/Quiz-Creator/create-quiz/create-quiz.component';
import { StepperComponent } from '../components/Quiz-Management/Quiz-Creator/stepper/stepper.component';
import { SelectQuizComponent } from '../components/Game-Session/select-quiz/select-quiz.component';
import { CreateGameLayout } from '../components/Game-Session/create-game-layout/create-game-layout.component';
import { SelectDifficultyComponent } from '../components/Game-Session/select-difficulty/select-difficulty.component';
import { GameSessionComponent } from '../components/Game-Session/game-session/game-session.component';
import { GameResultsComponent } from '../components/Game-Session/game-results/game-results.component';
import { LeaderboardComponent } from '../components/Leaderboard/leaderboard/leaderboard.component';
import { LoginComponent } from '../components/Auth/login/login.component';
import { SignUpComponent } from '../components/Auth/sign-up/sign-up.component';

export const routes: Routes = [
  { path: '', component: MainMenuComponent },
  {
    path: 'play',
    component: CreateGameLayout,
    children: [
      { path: '', component: SelectQuizComponent },
      { path: 'difficulty', component: SelectDifficultyComponent },
      { path: 'session', component: GameSessionComponent },
      { path: 'results', component: GameResultsComponent },
    ],
  },
  {
    path: 'quiz-management',
    component: QuizManagerLayout,
    children: [
      { path: '', component: QuizListComponent },
      {
        path: 'create',
        children: [
          { path: '', component: CreateQuizComponent },
          { path: 'steps', component: StepperComponent },
        ],
      },
      { path: ':id', component: QuizDetailsComponent },
    ],
  },
  {
    path: 'leaderboard',
    component: LeaderboardComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: SignUpComponent,
  },
];
