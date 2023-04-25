import React, { useEffect, useState } from 'react'
import { AppBar } from '@material-ui/core'
import Toolbar from '@material-ui/core/Toolbar'
import Searchbox from '../searchbox/Searchbox'
import { createMuiTheme, ThemeProvider } from '@material-ui/core/styles'
import logo from '../logo.png'
import './Topbar.css'
import Profile from '../profile/Profile'
import { useHistory } from 'react-router-dom'
import authService from '../../../services/auth.service'
const Theme = createMuiTheme({
  palette: {
    primary: {
      main: '#292525'
    }
  }
})
export default function Topbar({ content }) {
  const [render, setRender] = useState(true)
  const [user, setUser] = useState({})
  let history = useHistory()

  /*Dit zorgt ervoor dat de topbar niet rendered op de register en login pagina*/
  useEffect(() => {
    authService.getCurrentUser().then((data) => setUser(data))

    if (history.location.pathname.toLowerCase() === '/register' || history.location.pathname.toLowerCase() === '/login') {
      setRender(false)
    } else {
      setRender(true)
    }
    // eslint-disable-next-line
  }, [history])

  return render ? (
    <div>
      <ThemeProvider theme={Theme}>
        <AppBar color='primary'>
          <Toolbar>
            <div className='toolbaritems'>
              <div className='logoContainer'>
                <img src={logo} className='logo' alt='Logo' onClick={() => history.push('/')} />
              </div>
              <Searchbox className='searchbox' />
              <Profile className='profile' user={user} />
            </div>
          </Toolbar>
        </AppBar>
      </ThemeProvider>
      <div className='content'>{content}</div>
    </div>
  ) : (
    <div>{content}</div>
  )
}
