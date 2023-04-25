import React from 'react'
import App from '../App.js'
import { act } from 'react-dom/test-utils'
import '@testing-library/jest-dom'
import { BrowserRouter } from 'react-router-dom'
import AlertTemplate from 'react-alert-template-basic'
import { transitions, Provider as AlertProvider } from 'react-alert'

import { mount, shallow, render } from 'enzyme'
import authService from '../services/auth.service.js'
import Topbar from '../components/topbar/topbar/Topbar.js'
// optional configuration
const options = {
  position: 'bottom center',
  timeout: 50000,
  offset: '30px',
  transition: transitions.SCALE
}
it.only('name', () => {})
it('Loading goes away', async () => {
  let component
  await act(async () => {
    await authService.login('user', 'd')
  })
  act(() => {
    component = mount(
      <AlertProvider template={AlertTemplate} {...options}>
        <BrowserRouter>
          <Topbar />
        </BrowserRouter>
      </AlertProvider>
    )
  })
  await act(async () => {
    await new Promise((res) => setTimeout(res, 1000))
  })

  component.find('[data-testid="profileName"]').forEach((e) => {
    expect(e.text()).toBe('testdisplay')
  })
})
