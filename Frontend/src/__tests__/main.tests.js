import React from 'react'
import App from '../App.js'
import { act } from 'react-dom/test-utils'
import '@testing-library/jest-dom'
import { BrowserRouter } from 'react-router-dom'
import WritepostBar from '../components/writepostbar/WritepostBar.js'
import AlertTemplate from 'react-alert-template-basic'
import { transitions, Provider as AlertProvider } from 'react-alert'

import { mount, shallow, render } from 'enzyme'
import { waitForElement, waitForElementToBeRemoved } from '@testing-library/react'
import Mainpage from '../pages/mainpage/Mainpage.js'
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
it('Initial page shows loading', async () => {
  let component
  act(() => {
    component = mount(
      <AlertProvider template={AlertTemplate} {...options}>
        <BrowserRouter>
          <Mainpage />
        </BrowserRouter>
      </AlertProvider>
    )
  })
  expect(component.find("[data-testid='loading']").exists()).toBeTruthy()
})

it('Renders without errors', () => {
  act(() => {
    render(<App />)
  })
})

it('Unauthorized shows login page', async () => {
  let render
  await act(async () => {
    render = mount(
      <BrowserRouter initialEntries={['/account']}>
        <App />
      </BrowserRouter>
    )
  })

  expect(render.find("[data-testid='login-form']")).not.toBeNull()
})
