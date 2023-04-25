import React from 'react'
import { fireEvent, render, wait, waitForElementToBeRemoved } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import App from '../App.js'
import { act } from 'react-dom/test-utils'
import '@testing-library/jest-dom'
import { BrowserRouter } from 'react-router-dom'

it.only('name', () => {})
it('loginpage can login', async () => {
  await act(async () => {
    const { getByTestId, getByText } = render(
      <BrowserRouter initialEntries={['/login']}>
        <App />
      </BrowserRouter>
    )
    await waitForElementToBeRemoved(() => getByText('Loading'))
    var form = getByTestId('login-form')
    userEvent.type(form.querySelector('input[id="username"]'), 'user')
    userEvent.type(form.querySelector('input[id="password"]'), 'd')

    userEvent.click(form.querySelector('button[id="submitButton"'))
  })
  wait(() => {
    expect(localStorage.getItem('user')).not.toBeNull()
  }, 1000)
})

it('loginpage false login does not set localstorage', async () => {
  await act(async () => {
    const { getByTestId, getByText } = render(
      <BrowserRouter initialEntries={['/login']}>
        <App />
      </BrowserRouter>
    )

    var form = getByTestId('login-form')
    userEvent.type(form.querySelector('input[id="username"]'), '')
    userEvent.type(form.querySelector('input[id="password"]'), '')

    userEvent.click(form.querySelector('button[id="submitButton"'))
  })
  wait(() => {
    expect(localStorage.getItem('user')).toBeNull()
  }, 1000)
})
