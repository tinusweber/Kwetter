import React, { useState, useEffect } from 'react'
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'
import Index from './pages/mainpage/Mainpage'
import NotFoundPage from './pages/404/NotFoundPage'
import Profilepage from './pages/profilepage/Profilepage'
import Topbar from '../src/components/topbar/topbar/Topbar'
import SearchPage from './pages/searchpage/SearchPage'
import LoginPage from './pages/loginpage/Loginpage'
import AuthService from './services/auth.service'
import Registerpage from './pages/registerpage/Registerpage'
import Accountpage from './pages/accountpage/Accountpage'
import SupportPage from './pages/supportpage/SupportPage'

export default function App() {
  const [loggedin, setLoggedIn] = useState(false)
  const [loading, setLoading] = useState(true)

  const PrivateRoute = ({ children, ...rest }) => {
    return (
      <Route
        {...rest}
        render={({ location }) =>
          !loading ? (
            loggedin ? (
              children
            ) : (
              <Redirect
                to={{
                  pathname: '/login',
                  state: { from: location }
                }}
              />
            )
          ) : (
            <div>Loading</div>
          )
        }
      />
    )
  }

  useEffect(() => {
    AuthService.isLoggedin().then((data) => {
      setLoggedIn(data)
      setLoading(false)
    })
  })

  return (
    <div>
      <Router>
        <Topbar
          content={
            <Switch>
              <PrivateRoute exact path='/'>
                <Index />
              </PrivateRoute>
              <Route exact path='/404'>
                <NotFoundPage />
              </Route>
              <PrivateRoute exact path='/user/:id'>
                <Profilepage />
              </PrivateRoute>
              <PrivateRoute exact path='/search/:query'>
                <SearchPage />
              </PrivateRoute>
              <PrivateRoute exact path='/account'>
                <Accountpage />
              </PrivateRoute>

              <PrivateRoute exact path='/support'>
                <SupportPage />
              </PrivateRoute>
              <Route exact path='/register'>
                <Registerpage />
              </Route>

              <Route exact path='/login'>
                <LoginPage />
              </Route>
              <Redirect to='/404' />
            </Switch>
          }
        />
      </Router>
    </div>
  )
}
