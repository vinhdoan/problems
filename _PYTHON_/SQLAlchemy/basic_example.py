#!/usr/bin/env python
import sqlalchemy  # 1
from sqlalchemy import create_engine  # 2
from sqlalchemy.ext.declarative import declarative_base  # 3
from sqlalchemy import Column, Integer, String  # 3
from sqlalchemy import Sequence  # 3
from sqlalchemy.orm import sessionmaker  # 6
from sqlalchemy.orm import aliased  # 9, 12
from sqlalchemy import text  # 9
from sqlalchemy import func  # 9, 12
from sqlalchemy import ForeignKey  # 10
from sqlalchemy.orm import relationship  # 10
from sqlalchemy.sql import exists  # 12
from sqlalchemy.orm import subqueryload  # 13
from sqlalchemy.orm import joinedload  # 13
from sqlalchemy.orm import contains_eager  # 13


# 1. SQLAlchemy version
print "*** 1. SQLAlchemy version"
print sqlalchemy.__version__
print

# 2. Connecting
print "*** 2. Connecting"
engine = create_engine('sqlite:///:memory:', echo=True)  # True: show SQL cmd
print

# 3. Declare a Mapping
print "*** 3. Declare a Mapping"
Base = declarative_base()


class User(Base):
    __tablename__ = 'users'

    id = Column(Integer, Sequence('user_id_seq'), primary_key=True)
    name = Column(String(50))
    fullname = Column(String(50))
    password = Column(String(12))

    def __repr__(self):
        return "<User(name='%s', fullname='%s', password='%s')>"\
            % (self.name, self.fullname, self.password)


print

# 4. Create a Schema
print "*** 4. Create a Schema"
Base.metadata.create_all(engine)
print

# 5. Create an Instance of the Mapped Class
print "*** 5. Create an Instance of the Mapped Class"
ed_user = User(name='ed', fullname='Ed Jones', password='edspassword')
print ed_user.name
print ed_user.password
print str(ed_user.id)
print

# 6. Creating a Session
print "*** 6. Creating a Session"
Session = sessionmaker(bind=engine)
session = Session()  # whenever you need to have a conversation with the DB
print

# 7. Adding and Updating Objects
print "*** 7. Adding and Updating Objects"
print "--- ADD A USER"
session.add(ed_user)
print
print "--- QUERY THE USER"
our_user = session.query(User).filter_by(name='ed').first()
print
print "--- PRINT QUERY RESULT AND COMPARE"
print our_user
print ed_user is our_user
print
print "--- ADD MULTIPLE USERS"
session.add_all([
    User(name='wendy', fullname='Wendy Williams', password='foobar'),
    User(name='mary', fullname='Mary Contrary', password='xxg527'),
    User(name='fred', fullname='Fred Flinstone', password='blah')])
print
print "--- CHANGE PASSWORD OF A USER"
ed_user.password = 'f8s7ccs'
print
print "--- SHOW THE CHANGES SO FAR"
print session.dirty
print session.new
print
print "--- COMMIT THE CHANGES"
session.commit()
print
print "--- SHOW ID OF A USER AFTER COMMITTING"
print ed_user.id
print

# 8. Rolling Back
print "*** 8. Rolling Back"
print "--- MAKE SOME CHANGES"
ed_user.name = 'Edwardo'
fake_user = User(name='fakeuser', fullname='Invalid', password='12345')
session.add(fake_user)
print
print "--- CHECK IF CHANGES COMMITTED BY QUERYING"
print session.query(User).filter(User.name.in_(['Edwardo', 'fakeuser'])).all()
print
print "--- ROLLBACK"
session.rollback()
print
print "--- CHECK VALUES AFTER ROLLBACK"
print ed_user.name
print fake_user in session
print
print "--- CHECK IF ROLLED BACK BY QUERYING"
print session.query(User).filter(User.name.in_(['ed', 'fakeuser'])).all()
print

# 9. Querying
print "*** 9. Querying"
print "--- EVALUATED IN AN ITERATIVE CONTEXT"
for instance in session.query(User).order_by(User.id):
    print instance.name, instance.fullname
print
print "--- EXPRESSED AS TUPLE (1)"
for name, fName, pwd in session.query(User.name, User.fullname, User.password):
    print name, fName, pwd
print
print "--- EXPRESSED AS TUPLE (2) - NAMED TUPLE"
for row in session.query(User, User.name).all():
    print row.User, row.name
print
print "--- EXPRESSED AS TUPLE (3) - NAMED TUPLE with LABEL"
for row in session.query(User.name.label('name_label')).all():
    print(row.name_label)
print
print "--- ALIASED"
user_alias = aliased(User, name='user_alias')
for row in session.query(user_alias, user_alias.name).all():
    print(row.user_alias)
print
print "--- BASIC OPERATIONS (1) - LIMIT, OFFSET"
for u in session.query(User).order_by(User.id)[1:3]:
    print(u)
print
print "--- BASIC OPERATIONS (2) - FILTER_BY"
for name, in session.query(User.name).filter_by(fullname='Ed Jones'):
    print(name)
print
print "--- BASIC OPERATIONS (3) - FILTER"
for name, in session.query(User.name).filter(User.fullname == 'Ed Jones'):
    print(name)
print
print "--- BASIC OPERATIONS (4) - MULTI FILTER"
for user in session.query(User)\
                   .filter(User.name == 'ed')\
                   .filter(User.fullname == 'Ed Jones'):
    print(user)
print
print "--- TEXTUAL SQL (1)"
for user in session.query(User)\
                   .filter(text("id<224"))\
                   .order_by(text("id")).all():
    print(user.name)
print
print "--- TEXTUAL SQL (2) - with PARAMS"
session.query(User).filter(text("id<:value and name=:name"))\
                   .params(value=224, name='fred')\
                   .order_by(User.id)\
                   .one()
print
print "--- TEXTUAL SQL (3) - ENTIRELY STRING-BASED"
print session.query(User)\
             .from_statement(text("SELECT * FROM users where name=:name"))\
             .params(name='ed')\
             .all()
print
print "--- TEXTUAL SQL (4) - ENTIRELY STRING-BASED, PASSING COLUMN"
stmt = text("SELECT name, id, fullname, password FROM users where name=:name")
stmt = stmt.columns(User.name, User.id, User.fullname, User.password)
print session.query(User).from_statement(stmt).params(name='ed').all()
print
print "--- TEXTUAL SQL (5) - ENTIRELY STRING-BASED, COLUMNS SPECIFIED"
stmt = text("SELECT name, id FROM users where name=:name")
stmt = stmt.columns(User.name, User.id)
print session.query(User.id, User.name)\
             .from_statement(stmt)\
             .params(name='ed')\
             .all()
print
print "--- COUNTING (1)"
print session.query(User).filter(User.name.like('%ed')).count()
print
print "--- COUNTING (2) - FUNC COUNT"
print session.query(func.count(User.name), User.name).group_by(User.name).all()
print
print "--- COUNTING (3) - FUNC COUNT *"
print session.query(func.count('*')).select_from(User).scalar()  # option 1
print session.query(func.count(User.id)).scalar()  # option 2 (primary key)
print

# 10. Building a Relationship
print "*** 10. Building a Relationship"
print "--- DEFINE NEW TABLE"


class Address(Base):
    __tablename__ = 'addresses'
    id = Column(Integer, primary_key=True)
    email_address = Column(String, nullable=False)
    user_id = Column(Integer, ForeignKey('users.id'))

    user = relationship("User", back_populates="addresses")

    def __repr__(self):
        return "<Address(email_address='%s')>" % self.email_address


User.addresses = relationship("Address",
                              order_by=Address.id,
                              back_populates="user")
print
print "--- UPDATE TABLES"
Base.metadata.create_all(engine)
print

# 11. Working with Related Objects
print "*** 11. Working with Related Objects"
print "--- NEW USER"
jack = User(name='jack', fullname='Jack Bean', password='gjffdd')
print
print "--- NO ADDRESSES FIELD SO FAR"
print jack.addresses
print
print "--- ADD ADDRESSES FIELD"
jack.addresses = [
    Address(email_address='jack@google.com'),
    Address(email_address='j25@yahoo.com')]
print jack.addresses[1]
print jack.addresses[1].user
print
print "--- COMMIT TO DB"
session.add(jack)
session.commit()
print
print "--- QUERY USER TO VERIFY"
jack = session.query(User).filter_by(name='jack').one()
print jack
print
print "--- VERIFY ADDRESSES (LATE LOADING)"
print jack.addresses
print

# 12. Querying with Joins
print "*** 12. Querying with Joins"
print "--- SIMPLE IMPLICIT JOIN"
for u, a in session.query(User, Address)\
                   .filter(User.id == Address.user_id)\
                   .filter(Address.email_address == 'jack@google.com')\
                   .all():
    print(u)
    print(a)
print
print "--- ACTUAL SQL JOIN SYNTAX - IN CASE OF FOREIGN KEY PROVIDED"
session.query(User)\
       .join(Address)\
       .filter(Address.email_address == 'jack@google.com')\
       .all()
print
print "--- ACTUAL SQL JOIN SYNTAX - IN CASE OF FOREIGN KEY NOT PROVIDED"
# query.join(Address, User.id == Address.user_id)  # explicit condition
# query.join(User.addresses)                       # specify relationship L2R
# query.join(Address, User.addresses)              # same, with explicit target
# query.join('addresses')                          # same, using a string
# query.outerjoin(User.addresses)                  # LEFT OUTER JOIN
print
print "--- USING ALIASES"
adalias1 = aliased(Address)
adalias2 = aliased(Address)
for username, email1, email2 \
    in session.query(User.name,
                     adalias1.email_address,
                     adalias2.email_address)\
              .join(adalias1, User.addresses)\
              .join(adalias2, User.addresses)\
              .filter(adalias1.email_address == 'jack@google.com')\
              .filter(adalias2.email_address == 'j25@yahoo.com'):
    print username, email1, email2
print
print "--- USING SUBQUERIES"
stmt = session.query(Address.user_id,
                     func.count('*').label('address_count'))\
              .group_by(Address.user_id)\
              .subquery()
for u, count in session.query(User, stmt.c.address_count)\
                       .outerjoin(stmt, User.id == stmt.c.user_id)\
                       .order_by(User.id):
    print(u, count)
print
print "--- SELECTING ENTITIES FROM SUBQUERIES"
stmt = session.query(Address)\
              .filter(Address.email_address != 'j25@yahoo.com')\
              .subquery()
adalias = aliased(Address, stmt)
for user, address in session.query(User, adalias)\
                            .join(adalias, User.addresses):
    print(user)
    print(address)
print
print "--- USING EXISTS - EXPLICIT EXISTS CONSTRUCT"
stmt = exists().where(Address.user_id == User.id)
for name, in session.query(User.name).filter(stmt):
    print(name)
print
print "--- USING EXISTS (1) - VIA ANY"
for name, in session.query(User.name)\
                    .filter(User.addresses.any()):
    print(name)
print
print "--- USING EXISTS (2) - VIA ANY WITH CRITERIA"
for name, in\
    session.query(User.name)\
           .filter(User.addresses.any(Address.email_address.like('%google%'))):
    print(name)
print
print "--- USING EXISTS (3) - VIA HAS"
print session.query(Address)\
             .filter(~Address.user.has(User.name == 'jack'))\
             .all()  # ~ is NOT
print

# 13. Eager Loading
print "*** Eager Loading"
print "--- SUBQUERY LOAD"
jack = session.query(User)\
              .options(subqueryload(User.addresses))\
              .filter_by(name='jack')\
              .one()
print jack
print jack.addresses
print
print "--- JOINED LOAD"
jack = session.query(User)\
              .options(joinedload(User.addresses))\
              .filter_by(name='jack')\
              .one()
print jack
print jack.addresses
print
print "--- EXPLICIT JOIN + EAGERLOAD"
jacks_addresses = session.query(Address)\
                         .join(Address.user)\
                         .filter(User.name == 'jack')\
                         .options(contains_eager(Address.user))\
                         .all()
print jacks_addresses
print jacks_addresses[0].user
print

# 14. Deleting
print "*** Deleting"
print "--- WITHOUT CASCADE"
session.delete(jack)
print session.query(User).filter_by(name='jack').count()
print session.query(Address)\
             .filter(Address.email_address.in_(['jack@google.com',
                                                'j25@yahoo.com']))\
             .count()  # still exist
print
print "--- WITH CASCADE"
session.rollback()
User.addresses = relationship("Address",
                              back_populates='user',
                              cascade="all, delete, delete-orphan")
Base.metadata.create_all(engine)  # update table

# load Jack by primary key
jack = session.query(User).get(5)
# remove one Address (lazy load fires off)
del jack.addresses[1]
# only one address remains
print session.query(Address)\
             .filter(Address.email_address.in_(['jack@google.com',
                                                'j25@yahoo.com']))\
             .count()
session.delete(jack)
print session.query(User).filter_by(name='jack').count()
print session.query(Address)\
             .filter(Address.email_address.in_(['jack@google.com',
                                                'j25@yahoo.com']))\
             .count()
print
