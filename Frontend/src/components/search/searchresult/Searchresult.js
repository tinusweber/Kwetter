import React from 'react'
import Avatar from '@material-ui/core/Avatar'
import { Column, Row, Item } from '@mui-treasury/components/flex'
import { Info, InfoTitle, InfoSubtitle } from '@mui-treasury/components/info'
import { useChatzInfoStyles } from '@mui-treasury/styles/info/chatz'
import { useHistory } from 'react-router-dom'

function Searchresult({ result }) {
  let history = useHistory()
  return (
    <>
      <Column gap={2} onClick={() => history.push('/user/' + result.ownerId)}>
        <Row alignItems={'center'}>
          <Item position={'middle'}>
            <div>
              <Avatar src={result.profilePicture} />
            </div>
          </Item>
          <Info useStyles={useChatzInfoStyles}>
            <InfoTitle>{result.displayName}</InfoTitle>
            <InfoSubtitle>{result.biography}</InfoSubtitle>
          </Info>
        </Row>
      </Column>
    </>
  )
}

export default Searchresult
