테이블 생성 방법
1) 프로젝트.Domain Models 폴더에 테이블 속성 작성
2) 프로젝트.EntityFramework 에 DbContext, DbContextFactory 생성
3) 패키지 관리자 콘솔에서 기본 프로젝트 프로젝트.EntityFramework 설정 후 [add-migration init] 실행
4) update-database 실행